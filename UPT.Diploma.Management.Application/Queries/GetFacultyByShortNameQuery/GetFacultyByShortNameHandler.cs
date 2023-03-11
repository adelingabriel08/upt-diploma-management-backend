using AutoMapper;
using MediatR;
using UPT.Diploma.Management.Application.Exceptions;
using UPT.Diploma.Management.Application.Queries.Base;
using UPT.Diploma.Management.Application.ViewModels;
using UPT.Diploma.Management.Domain.Models;
using UPT.Diploma.Management.Persistence.Contracts;

namespace UPT.Diploma.Management.Application.Queries.GetFacultyByShortNameQuery;

public class GetFacultyByShortNameHandler : IRequestHandler<GetFacultyByShortNameQuery, BaseQueryResult<FacultyViewModel>>
{
    private readonly IBaseRepository<Faculty> _facultyRepository;
    private readonly IMapper _mapper;
    
    public GetFacultyByShortNameHandler(IBaseRepository<Faculty> facultyRepository, IMapper mapper)
    {
        _facultyRepository = facultyRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseQueryResult<FacultyViewModel>> Handle(GetFacultyByShortNameQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetFacultyByShortNameValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (validatorResult.Errors.Count > 0)
        {
            throw new ValidationException(validatorResult);
        }

        var faculty = (await _facultyRepository.GetAsync(f => f.ShortName == request.ShortName)).FirstOrDefault();
        
        if (faculty is null)
            throw new ValidationException("Prescurtarea facultății nu este validă!");

        var viewModel = _mapper.Map<FacultyViewModel>(faculty);

        return new BaseQueryResult<FacultyViewModel>() { QueryPayload = viewModel };

    }
}