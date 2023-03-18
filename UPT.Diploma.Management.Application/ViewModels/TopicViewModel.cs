using UPT.Diploma.Management.Application.ViewModels.Base;

namespace UPT.Diploma.Management.Application.ViewModels;

public record TopicViewModel : ViewModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProfessorId { get; set; }
    public int? CompanyId { get; set; }
    public int FacultyId { get; set; }
    
    public UserViewModel Professor { get; set; }
    public CompanyViewModel Company { get; set; }
}