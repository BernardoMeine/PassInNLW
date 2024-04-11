using Microsoft.EntityFrameworkCore;
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
public class CheckInRepository : ICheckInRepository
{
    private readonly PassInDbContext _context;

    public CheckInRepository(PassInDbContext context) => _context = context;

    public ResponseRegisteredJson DoAttendeeCheckIn(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId
        };

        _context.CheckIns.Add(entity);
        _context.SaveChanges();

        return new ResponseRegisteredJson
        {
            id = entity.Id
        };
    }

    public void Validate(Guid attendeeId)
    {
        var existAttendee = _context.Attendees.Any(at => at.Id == attendeeId);
        if (existAttendee == false)
        {
            throw new NotFoundException("O participante com esse Id não foi encontrado");
        }

        var existCheckin = _context.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
        if (existCheckin)
        {
            throw new ConflictException("Partipante não pode fazer CheckIn duas vezes no mesmo evento");
        }
    }
}
