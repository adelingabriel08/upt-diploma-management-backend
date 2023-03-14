using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.CreateTopicCmd;

public class CreateTopicCmdHandler : IRequestHandler<CreateTopicCmd, BaseResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBaseRepository<Faculty> _facultyRepository;
    private readonly IBaseRepository<Company> _companyRepository;
    private readonly IBaseRepository<Topic> _topicRepository;

    public CreateTopicCmdHandler(IHttpContextAccessor httpContextAccessor, IBaseRepository<Faculty> facultyRepository, 
        UserManager<ApplicationUser> userManager, IBaseRepository<Company> companyRepository, IBaseRepository<Topic> topicRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _facultyRepository = facultyRepository;
        _userManager = userManager;
        _companyRepository = companyRepository;
        _topicRepository = topicRepository;
    }

    public async Task<BaseResult> Handle(CreateTopicCmd request, CancellationToken cancellationToken)
    {
        var validator = new CreateTopicCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var faculty = await _facultyRepository.GetAsync(request.FacultyId);

        if (faculty is null)
            throw new ValidationException("Id-ul facultății este invalid!");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId);

        var user = await _userManager.FindByIdAsync(userId);
        
        if (user is null)
            throw new ValidationException("User invalid!");

        var topic = new Topic()
        {
            Name = request.Name,
            Description = request.Description,
            FacultyId = request.FacultyId
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        var hasCoordinatorAssigned = false;

        if (userRoles.Contains(Roles.Professor))
        {
            topic.ProfessorId = userId;
            hasCoordinatorAssigned = true;
        }

        if (userRoles.Contains(Roles.Company))
        {
            var company = (await _companyRepository.GetFirstAsync(c => c.UserId == userId));
            if (company is null)
                throw new ValidationException("User invalid!");
            
            hasCoordinatorAssigned = true;
            topic.CompanyId = company.Id;
        }
        
        if (!hasCoordinatorAssigned) 
            throw new ValidationException("User invalid!");

        await _topicRepository.AddAsync(topic);

        return new BaseResult();
    }
}