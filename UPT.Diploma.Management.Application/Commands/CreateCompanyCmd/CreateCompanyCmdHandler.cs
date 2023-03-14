using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.CreateCompanyCmd;

public class CreateCompanyCmdHandler : IRequestHandler<CreateCompanyCmd, BaseResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBaseRepository<Company> _companyRepository;

    public CreateCompanyCmdHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, 
        IBaseRepository<Company> companyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _companyRepository = companyRepository;
    }

    public async Task<BaseResult> Handle(CreateCompanyCmd request, CancellationToken cancellationToken)
    {
        var validator = new CreateCompanyCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            throw new ValidationException("User invalid!");

        var existingCompany = await _companyRepository.GetFirstAsync(x => x.UserId == userId);
        
        if (existingCompany != null)
            throw new ValidationException($"Ești asignat deja companiei {existingCompany.Name}! Nu poți crea mai multe companii!");


        var company = new Company()
        {
            Name = request.Name,
            LogoUrl = request.LogoUrl,
            ShortDescription = request.ShortDescription,
            UserId = userId
        };

        await _companyRepository.AddAsync(company);

        return new BaseResult();
    }
}