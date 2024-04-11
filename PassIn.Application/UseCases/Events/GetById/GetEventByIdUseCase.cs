using PassIn.Communication.Responses;
using PassIn.Infrastructure.Contracts;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    private readonly IEventRepository _repository;

    public GetEventByIdUseCase(IEventRepository repository) => _repository = repository;
    public ResponseEventJson Execute(Guid id) => _repository.GetEventById(id);
}
