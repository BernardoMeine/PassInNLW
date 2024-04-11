using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure.Contracts;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Repositories;
public class EventRepository : IEventRepository
{
    private readonly PassInDbContext _context;

    public EventRepository(PassInDbContext context) => _context = context;

    public ResponseEventJson GetEventById(Guid id)
    {
        var entity = _context.Events.Include(ev => ev.Attendees).FirstOrDefault(x => x.Id == id)
            ?? throw new NotFoundException("Um evento com esse Id não existe");


        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = entity.Attendees.Count
        };
    }

    public ResponseRegisteredJson RegisterEvent(RequestEventJson request)
    {
        Validate(request);

        var entity = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")
        };

        _context.Events.Add(entity);

        _context.SaveChanges();

        return new ResponseRegisteredJson
        {
            id = entity.Id
        };
    }

    public void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
        {
            throw new ErrorOnValidationException("O evento precisa ter no mínimo 1 participante");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ErrorOnValidationException("O evento precisa ter um título");
        }

        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ErrorOnValidationException("O evento precisa ter os detalhes");
        }
    }
}
