using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Commands.CreateTopicCmd;
using UPT.Diploma.Management.Application.Commands.DeleteTopicCmd;
using UPT.Diploma.Management.Application.Commands.EditTopicCmd;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.Queries.GetTopicsQuery;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Retrieves the topics paginated based on the user role.
    /// The students can get all the topics.
    /// The professor or company employee will be able to get only the topics they own.
    /// </summary>
    /// <param name="skip">Number of topics to be skipped.</param>
    /// <param name="take">Number of topics to be taken.</param>
    /// <returns>A list of topics.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseQueryResult<List<TopicViewModel>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> GetTopics(int skip, int take)
    {
        var cmd = new GetTopicsQuery() { Skip = skip, Take = take };
        return Ok(await _mediator.Send(cmd));
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