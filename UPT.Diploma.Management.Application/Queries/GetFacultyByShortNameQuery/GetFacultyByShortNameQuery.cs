using MediatR;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.GetFacultyByShortNameQuery;

public record GetFacultyByShortNameQuery : IRequest<BaseQueryResult<FacultyViewModel>>
{
    public string ShortName { get; set; }
}