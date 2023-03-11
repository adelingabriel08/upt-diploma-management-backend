using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Queries.TestFlowQuery;

namespace UPT.Diploma.Management.Api.Controllers;

[ApiController]
[Route("api/testflow")]
public class TestFlowController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestFlowController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("query")]
    public async Task<IActionResult> TestFlowQuery(string message)
    {
        var query = new TestFlowQuery() { Message = message };
        return Ok(await _mediator.Send(query));
    }
}