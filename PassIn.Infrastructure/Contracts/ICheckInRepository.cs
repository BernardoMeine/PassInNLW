using PassIn.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Contracts;
public interface ICheckInRepository
{
    ResponseRegisteredJson DoAttendeeCheckIn(Guid attendeeId);
    void Validate(Guid attendeeId);
}
