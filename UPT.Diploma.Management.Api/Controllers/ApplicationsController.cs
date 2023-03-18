using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Commands.CreateApplicationCmd;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Allows students to add an application.
    /// </summary>
    /// <param name="ProfessorId">The id of the coordinator professor</param>
    /// <param name="TopicId">The id of the topic</param>
    /// <param name="Observations">Some relevant student observations</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/applications
    ///
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = Roles.Student)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> CreateApplication(CreateApplicationCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
}