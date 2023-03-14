using MediatR;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;

namespace UPT.Diploma.Management.Application.Queries.GetCompanyByIdQuery;

public class GetCompanyByIdQuery : IRequest<BaseQueryResult<CompanyViewModel>>
{
    public int Id { get; set; }
}