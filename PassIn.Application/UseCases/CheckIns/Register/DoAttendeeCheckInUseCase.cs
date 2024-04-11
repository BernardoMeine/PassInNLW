using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Contracts;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.Register;
public class DoAttendeeCheckInUseCase
{
    private readonly ICheckInRepository _repository;

    public DoAttendeeCheckInUseCase(ICheckInRepository repository) => _repository = repository;

    public ResponseRegisteredJson Execute(Guid attendeeId) => _repository.DoAttendeeCheckIn(attendeeId);
}
