using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Contracts;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Repositories;
public class AttendeeRepository : IAttendeeRepository
{
    private readonly PassInDbContext _context;
    public AttendeeRepository(PassInDbContext context) => _context = context;
    public ResponseAllAttendeesJson GetAll(Guid eventId)
    {
        var entity = _context.Events.Include(ev => ev.Attendees).ThenInclude(at => at.CheckIn).FirstOrDefault(ev => ev.Id == eventId)
           ?? throw new NotFoundException("Um evento com esse Id não existe");

        return new ResponseAllAttendeesJson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at

            }).ToList()
        };
    }

    public ResponseRegisteredJson RegisterAttendee(Guid eventId, RequestRegisterEventJson request)
    {
        ValidateEvent(eventId, request);

        var entity = new Attendee
        {
            Name = request.Name,
            Email = request.Email,
            Event_Id = eventId
        };

        _context.Attendees.Add(entity);
        _context.SaveChanges();

        return new ResponseRegisteredJson
        {
            id = eventId
        };
    }

    public void ValidateEvent(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _context.Events.Find(eventId) ??
            throw new NotFoundException("Um evento com esse Id não existe");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("O nome é inválido");


        if (EmaisIsValid(request.Email) == false)
            throw new ErrorOnValidationException("O email é inválido");

        var attendeeAlreadyRegistered = _context
            .Attendees
            .Any(x => x.Event_Id == eventId && x.Email.Equals(request.Email));

        if (attendeeAlreadyRegistered)
            throw new ConflictException("Participante já inscrito neste evento");

        var attendeesCount = _context.Attendees.Count(x => x.Event_Id == eventId);
        if (attendeesCount == eventEntity.Maximum_Attendees)
            throw new ErrorOnValidationException("Número máximo de participantes atingido");
    }

    private static bool EmaisIsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
