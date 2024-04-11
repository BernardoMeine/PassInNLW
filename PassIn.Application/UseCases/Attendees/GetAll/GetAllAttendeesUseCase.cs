using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Contracts;

namespace PassIn.Application.UseCases.Attendees.GetAll;
public class GetAllAttendeesUseCase
{
    private readonly IAttendeeRepository _repository;

    public GetAllAttendeesUseCase(IAttendeeRepository repository) => _repository = repository;

    public ResponseAllAttendeesJson Execute(Guid eventId) => _repository.GetAll(eventId);
}
