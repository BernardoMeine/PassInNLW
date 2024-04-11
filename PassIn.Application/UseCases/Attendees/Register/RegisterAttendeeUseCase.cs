using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Contracts;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Attendees.Register;
public  class RegisterAttendeeUseCase
{
    private readonly IAttendeeRepository _repository;

    public RegisterAttendeeUseCase(IAttendeeRepository repository) => _repository = repository;

    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
    {
        _repository.RegisterAttendee(eventId, request);

        return new ResponseRegisteredJson
        {
            id = eventId
        };
    }
}
