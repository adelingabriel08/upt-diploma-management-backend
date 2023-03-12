using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.Queries.GetFacultyByShortNameQuery;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/faculties")]
public class FacultiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FacultiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves the faculty with the specified short name.
    /// </summary>
    /// <param name="shortName"></param>
    /// <returns>The faculty with the specified short name.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/faculties/ac
    ///
    /// </remarks>
    [HttpGet("{shortName}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseQueryResult<FacultyViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> GetFacultyByShortName(string shortName)
    {
        var query = new GetFacultyByShortNameQuery() { ShortName = shortName };
        return Ok(await _mediator.Send(query));
    }
}