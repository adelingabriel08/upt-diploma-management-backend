using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Domain.Models;

public class Application : EntityBase
{
    public string ProfessorId { get; set; }
    public int StudentId { get; set; }
    public int? TopicId { get; set; }
    public bool? Approved { get; set; }
    public string? Observations { get; set; }
    public ApplicationUser? Professor { get; set; }
    public Student? Student { get; set; }
    public Topic? Topic { get; set; }
}