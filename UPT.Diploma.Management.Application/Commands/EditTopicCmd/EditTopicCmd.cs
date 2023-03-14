using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.EditTopicCmd;

public class EditTopicCmd : IRequest<BaseResult>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FacultyId { get; set; }
}