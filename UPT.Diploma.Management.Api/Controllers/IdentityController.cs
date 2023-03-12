using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Commands.LoginCommand;
using UPT.Diploma.Management.Application.Commands.RegisterCommand;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Creates an user account if it does not exist.
    /// </summary>
    /// <returns>The jwt token used to authenticate in future requests.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/identity/register
    ///
    /// </remarks>
    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> Register(RegisterCmd registerCommand)
    {
        return Ok(await _mediator.Send(registerCommand));
    }
    
    /// <summary>
    /// Allows an user to login through the application.
    /// </summary>
    /// <returns>The jwt token used to authenticate in future requests.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/identity/login
    ///
    /// </remarks>
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsViewModel))]
    public async Task<IActionResult> Login(LoginCmd loginCommand)
    {
        return Ok(await _mediator.Send(loginCommand));
    }
}