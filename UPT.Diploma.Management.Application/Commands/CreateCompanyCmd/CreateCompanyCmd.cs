using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.CreateCompanyCmd;

public class CreateCompanyCmd : IRequest<BaseResult>
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string ShortDescription { get; set; }
}