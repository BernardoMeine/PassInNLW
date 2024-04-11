using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Application.UseCases.Attendees.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [Route("{eventId}/register")]
    public IActionResult RegisterAttendee(
        [FromBody] RequestRegisterEventJson request,
        [FromRoute] Guid eventId,
        [FromServices] RegisterAttendeeUseCase useCase)
    {
        var response = useCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll(
        [FromServices] GetAllAttendeesUseCase useCase,
        [FromRoute] Guid eventId)
    {
        var response = useCase.Execute(eventId);

        return Ok(response);
    }
}
