using MediatR;
using UPT.Diploma.Management.Application.Commands.Base;

namespace UPT.Diploma.Management.Application.Commands.DeleteTopicCmd;

public class DeleteTopicCmd : IRequest<BaseResult>
{
    public int Id { get; set; }
}