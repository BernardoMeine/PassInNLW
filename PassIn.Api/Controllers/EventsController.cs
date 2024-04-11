using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register(
        [FromBody] RequestEventJson request,
        [FromServices] RegisterEventUseCase useCase)
    {
        useCase.Execute(request);

        return Created(string.Empty, request);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById(
        [FromRoute] Guid id,
        [FromServices] GetEventByIdUseCase useCase)
    {
        var respose = useCase.Execute(id);

        return Ok(respose);
    }
}
