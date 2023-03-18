using AutoMapper;
using UPT.Diploma.Management.Application.ViewModels;
using UPT.Diploma.Management.Domain.Models;

namespace UPT.Diploma.Management.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Faculty, FacultyViewModel>();
        CreateMap<FacultyViewModel, Faculty>();
        CreateMap<Company, CompanyViewModel>();
        CreateMap<CompanyViewModel, Company>();
        CreateMap<ApplicationUser, UserViewModel>();
        CreateMap<Topic, TopicViewModel>();
        CreateMap<TopicViewModel, Topic>();
    }
}