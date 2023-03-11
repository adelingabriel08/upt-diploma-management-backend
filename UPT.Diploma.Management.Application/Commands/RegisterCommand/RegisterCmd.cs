using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.RegisterCommand;

public class RegisterCmd : IRequest<TokenResult>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}