using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.CreateApplicationCmd;

public class CreateApplicationCmd : IRequest<BaseResult>
{
    public string? ProfessorId { get; set; }
    public int? TopicId { get; set; }
    public string? Observations { get; set; }
}