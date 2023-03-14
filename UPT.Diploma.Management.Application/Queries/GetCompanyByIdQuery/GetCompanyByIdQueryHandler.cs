using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Queries.GetCompanyByIdQuery;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, BaseQueryResult<CompanyViewModel>>
{
    private readonly IBaseRepository<Company> _companyRepository;
    private readonly IMapper _mapper;
    
    public GetCompanyByIdQueryHandler(IBaseRepository<Company> companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseQueryResult<CompanyViewModel>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetCompanyByIdQueryValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var company = await _companyRepository.GetAsync(request.Id);

        if (company is null)
            throw new ValidationException("Compania nu exista sau id-ul este invalid!");
        
        var viewModel = _mapper.Map<CompanyViewModel>(company);

        return new BaseQueryResult<CompanyViewModel>() { QueryPayload = viewModel }; 

    }
}