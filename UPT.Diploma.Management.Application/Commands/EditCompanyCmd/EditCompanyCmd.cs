using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.EditCompanyCmd;

public class EditCompanyCmd : IRequest<BaseResult>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string ShortDescription { get; set; }
}