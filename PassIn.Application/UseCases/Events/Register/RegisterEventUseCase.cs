using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure.Contracts;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    private readonly IEventRepository _repository;

    public RegisterEventUseCase(IEventRepository repository) => _repository = repository;

    public ResponseRegisteredJson Execute(RequestEventJson request) => _repository.RegisterEvent(request);
};
