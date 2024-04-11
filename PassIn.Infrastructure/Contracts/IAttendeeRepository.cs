using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Contracts;
public interface IAttendeeRepository
{
    ResponseAllAttendeesJson GetAll(Guid eventId);
    ResponseRegisteredJson RegisterAttendee(Guid eventId, RequestRegisterEventJson request);
    void ValidateEvent(Guid eventId, RequestRegisterEventJson request);

}
