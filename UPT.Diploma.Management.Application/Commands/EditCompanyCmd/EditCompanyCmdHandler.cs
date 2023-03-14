using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.EditCompanyCmd;

public class EditCompanyCmdHandler : IRequestHandler<EditCompanyCmd, BaseResult>
{
    private readonly IBaseRepository<Company> _companyRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditCompanyCmdHandler(IBaseRepository<Company> companyRepository, IHttpContextAccessor httpContextAccessor)
    {
        _companyRepository = companyRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResult> Handle(EditCompanyCmd request, CancellationToken cancellationToken)
    {
        var validator = new EditCompanyCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId);

        var existingCompany = await _companyRepository.GetAsync(request.Id, false);
        
        if (existingCompany is null)
            throw new ValidationException($"Compania nu există!");
        
        if (existingCompany.UserId != userId)
            throw new ValidationException($"Nu poți edita această companie!");


        existingCompany.Name = request.Name;
        existingCompany.ShortDescription = request.ShortDescription;
        existingCompany.LogoUrl = request.LogoUrl;
        existingCompany.UpdatedTimeUtc = DateTime.UtcNow;

        await _companyRepository.CommitDbTransactionAsync();

        return new BaseResult();
    }
}