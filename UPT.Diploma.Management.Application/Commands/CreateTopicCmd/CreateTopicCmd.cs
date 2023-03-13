using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.CreateTopicCmd;

public class CreateTopicCmd : IRequest<BaseResult>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int FacultyId { get; set; }
}