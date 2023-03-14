using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.EditTopicCmd;

public class EditTopicCmdHandler : IRequestHandler<EditTopicCmd, BaseResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Faculty> _facultyRepository;
    private readonly IBaseRepository<Topic> _topicRepository;
    private readonly IBaseRepository<Company> _companyRepository;

    public EditTopicCmdHandler(IHttpContextAccessor httpContextAccessor, IBaseRepository<Faculty> facultyRepository, 
        IBaseRepository<Topic> topicRepository, IBaseRepository<Company> companyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _facultyRepository = facultyRepository;
        _topicRepository = topicRepository;
        _companyRepository = companyRepository;
    }
    
    public async Task<BaseResult> Handle(EditTopicCmd request, CancellationToken cancellationToken)
    {
        var validator = new EditTopicCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var faculty = await _facultyRepository.GetAsync(request.FacultyId);

        if (faculty is null)
            throw new ValidationException("Id-ul facultății este invalid!");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId);

        var existingTopic = await _topicRepository.GetAsync(request.Id, false);

        if (existingTopic is null)
            throw new ValidationException("Id-ul temei este invalid!");

        if (existingTopic.ProfessorId != userId)
        {
            var company = await _companyRepository.GetFirstAsync(p => p.UserId == userId);

            if (company is null)
                throw new ValidationException("Nu poți edita această temă!");
        }

        existingTopic.Name = request.Name;
        existingTopic.Description = request.Description;
        existingTopic.FacultyId = request.FacultyId;
        existingTopic.UpdatedTimeUtc = DateTime.UtcNow;

        await _topicRepository.CommitDbTransactionAsync();

        return new BaseResult();
    }
}