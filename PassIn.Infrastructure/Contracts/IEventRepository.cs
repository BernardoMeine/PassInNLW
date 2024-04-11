using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Contracts;
public interface IEventRepository
{
    ResponseEventJson GetEventById(Guid id);
    ResponseRegisteredJson RegisterEvent(RequestEventJson request);
    void Validate(RequestEventJson request);
}
