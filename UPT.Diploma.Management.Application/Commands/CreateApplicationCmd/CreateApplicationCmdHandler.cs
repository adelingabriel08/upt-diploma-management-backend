using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using UPT.Diploma.Management.Application.Commands.Base;
using UPT.Diploma.Management.Application.Constants;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Commands.CreateApplicationCmd;

public class CreateApplicationCmdHandler : IRequestHandler<CreateApplicationCmd, BaseResult>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Domain.Models.Application> _applicationsRepository;
    private readonly IBaseRepository<Topic> _topicRepository;
    private readonly IBaseRepository<Student> _studentRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateApplicationCmdHandler(IHttpContextAccessor httpContextAccessor, 
        IBaseRepository<Domain.Models.Application> applicationsRepository, 
        IBaseRepository<Topic> topicRepository, 
        UserManager<ApplicationUser> userManager, 
        IBaseRepository<Student> studentRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _applicationsRepository = applicationsRepository;
        _topicRepository = topicRepository;
        _userManager = userManager;
        _studentRepository = studentRepository;
    }

    public async Task<BaseResult> Handle(CreateApplicationCmd request, CancellationToken cancellationToken)
    {
        var validator = new CreateApplicationCmdValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var student = await _studentRepository.GetFirstAsync(x =>
                x.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(CustomClaims.UserId));

        if (student is null)
            throw new ValidationException("Problemă cu profilul de student. Încearcă mai târziu!");

        if (request.TopicId.HasValue)
        {
            if (request.TopicId.Value <= 0) throw new ValidationException("Id-ul temei este invalid!");

            var topic = await _topicRepository.GetAsync(request.TopicId.Value);

            if (topic is null) throw new ValidationException("Id-ul temei este invalid!");

            string professorId;

            if (string.IsNullOrEmpty(topic.ProfessorId))
            {
                if (string.IsNullOrEmpty(request.ProfessorId))
                    throw new ValidationException("Alege și un profesor coordonator!");

                professorId = request.ProfessorId;
                var professor = await _userManager.FindByIdAsync(request.ProfessorId);

                if (professor is null)
                    throw new ValidationException("Id-ul profesorului coordonator este invalid!");
            }
            else
            {
                professorId = topic.ProfessorId;
            }

            var application = new Domain.Models.Application()
            {
                TopicId = topic.Id,
                Approved = false,
                Observations = request.Observations,
                ProfessorId = professorId,
                StudentId = student.Id
            };

            await _applicationsRepository.AddAsync(application);

            return new BaseResult();
        }

        if (!string.IsNullOrEmpty(request.ProfessorId))
        {
            var professor = await _userManager.FindByIdAsync(request.ProfessorId);

            if (professor is null)
                throw new ValidationException("Id-ul profesorului coordonator este invalid!");

            var existingProfessorApplication = await _applicationsRepository.GetFirstAsync(x =>
                x.ProfessorId == request.ProfessorId
                && x.TopicId == null);
            
            if (existingProfessorApplication != null)
                throw new ValidationException("Poți alege un singur profesor coordonator!");
            
            var application = new Domain.Models.Application()
            {
                Approved = false,
                Observations = request.Observations,
                ProfessorId = request.ProfessorId,
                StudentId = student.Id
            };

            await _applicationsRepository.AddAsync(application);

            return new BaseResult();
        }
        
        throw new ValidationException("Alege o temă sau un profesor coordonator!");
    }
}
