using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Commands.CreateTopicCmd;
using UPT.Diploma.Management.Application.Commands.DeleteTopicCmd;
using UPT.Diploma.Management.Application.Commands.EditTopicCmd;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[ApiController]
[Route("api/topic")]
public class TopicController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Allows professors or company employees to add a topic.
    /// </summary>
    /// <param name="Name">The name of the topic</param>
    /// <param name="Description">The description of the topic</param>
    /// <param name="FacultyId">The faculty that should be linked to the topic</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/topic
    ///
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = Roles.Company + "," + Roles.Professor)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> CreateTopic(CreateTopicCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
    
    /// <summary>
    /// Allows professors or company employees to edit a topic they own.
    /// </summary>
    /// <param name="Id">The id of the topic that needs an edit</param>
    /// <param name="Name">The name of the topic</param>
    /// <param name="Description">The description of the topic</param>
    /// <param name="FacultyId">The faculty that should be linked to the topic</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/topic
    ///
    /// </remarks>
    [HttpPut]
    [Authorize(Roles = Roles.Company + "," + Roles.Professor)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> EditTopic(EditTopicCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
    
    /// <summary>
    /// Allows professors or company employees to delete a topic they own.
    /// </summary>
    /// <param name="Id">The id of the topic that needs to be deleted</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/topic
    ///
    /// </remarks>
    [HttpDelete]
    [Authorize(Roles = Roles.Company + "," + Roles.Professor)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> DeleteTopic(DeleteTopicCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
}