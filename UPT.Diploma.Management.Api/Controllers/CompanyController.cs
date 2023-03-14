using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Commands.CreateCompanyCmd;
using UPT.Diploma.Management.Application.Commands.EditCompanyCmd;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Queries.GetCompanyByIdQuery;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Retrieves the company with the specified id.
    /// </summary>
    /// <param name="id">The id of the desired company.</param>
    /// <returns>The company with the specified id.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/company/1
    ///
    /// </remarks>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> GetCompany(int id)
    {
        var cmd = new GetCompanyByIdQuery() { Id = id };
        return Ok(await _mediator.Send(cmd));
    }

    /// <summary>
    /// Allows company employees to add details about their company
    /// </summary>
    /// <param name="Name">The name of the company</param>
    /// <param name="ShortDescription">A short description of the company</param>
    /// <param name="LogoUrl">An url to a company logo</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/company
    ///
    /// </remarks>
    [HttpPost]
    [Authorize(Roles = Roles.Company)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> CreateCompany(CreateCompanyCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
    
    /// <summary>
    /// Allows company employees to edit details about their company
    /// </summary>
    /// <param name="Id">The id of the company</param>
    /// <param name="Name">The name of the company</param>
    /// <param name="ShortDescription">A short description of the company</param>
    /// <param name="LogoUrl">An url to a company logo</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/company
    ///
    /// </remarks>
    [HttpPut]
    [Authorize(Roles = Roles.Company)]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> EditCompany(EditCompanyCmd cmd)
    {
        return Ok(await _mediator.Send(cmd));
    }
}