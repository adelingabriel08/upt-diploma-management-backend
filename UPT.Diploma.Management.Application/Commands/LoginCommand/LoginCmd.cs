using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.LoginCommand;

public class LoginCmd : IRequest<TokenResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}