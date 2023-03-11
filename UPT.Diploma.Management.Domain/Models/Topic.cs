using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Domain.Models;

public class Topic : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ProfessorId { get; set; }
    public int? CompanyId { get; set; }
    public int FacultyId { get; set; }
    
    public ApplicationUser? Professor { get; set; }
    public Company? Company { get; set; }
    public Faculty? Faculty { get; set; }
    public ICollection<Application>? Applications { get; set; }
}