using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Queries.GetTopicsQuery;

public class GetTopicsQueryHandler : IRequestHandler<GetTopicsQuery, BaseQueryResult<List<TopicViewModel>>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Topic> _topicRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetTopicsQueryHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, 
        IBaseRepository<Topic> topicRepository, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public async Task<BaseQueryResult<List<TopicViewModel>>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetTopicsQueryValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var user = await _userManager.FindByIdAsync(
            _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId));

        var userRoles = await _userManager.GetRolesAsync(user);

        List<Topic> topics = new List<Topic>();
        
        if (userRoles.Contains(Roles.Student))
        {
             topics = await _topicRepository.GetAsync(q => 
                q.Include(p => p.Company)
                    .Include(p => p.Professor), request.Skip, request.Take);
        }
        else
        {
            topics = await _topicRepository.GetAsync(q => 
                q.Include(p => p.Company)
                    .ThenInclude(p => p.User)
                    .Include(p => p.Professor)
                    .Where(x => x.ProfessorId == user.Id || x.Company.UserId == user.Id), 
                request.Skip, request.Take);
        }

        var viewModels = topics.Select(topic => _mapper.Map<TopicViewModel>(topic));

        return new BaseQueryResult<List<TopicViewModel>>() {QueryPayload = viewModels.ToList()};
    }
}