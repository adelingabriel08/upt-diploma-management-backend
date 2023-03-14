using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.DeleteTopicCmd;

public class DeleteTopicCmdHandler : IRequestHandler<DeleteTopicCmd, BaseResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Topic> _topicRepository;
    private readonly IBaseRepository<Company> _companyRepository;

    public DeleteTopicCmdHandler(IHttpContextAccessor httpContextAccessor, IBaseRepository<Topic> topicRepository, 
        IBaseRepository<Company> companyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _topicRepository = topicRepository;
        _companyRepository = companyRepository;
    }

    public async Task<BaseResult> Handle(DeleteTopicCmd request, CancellationToken cancellationToken)
    {
        var validator = new DeleteTopicCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }
        

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId);

        var existingTopic = await _topicRepository.GetAsync(request.Id, false);

        if (existingTopic is null)
            throw new ValidationException("Id-ul temei este invalid!");

        if (existingTopic.ProfessorId != userId)
        {
            var company = await _companyRepository.GetFirstAsync(p => p.UserId == userId);

            if (company is null)
                throw new ValidationException("Nu poți șterge această temă!");
        }
        

        await _topicRepository.DeleteAsync(existingTopic);

        return new BaseResult();
    }
}