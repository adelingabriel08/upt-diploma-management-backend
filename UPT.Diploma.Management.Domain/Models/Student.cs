using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Domain.Models;

public class Student : EntityBase
{
    public string StudentIdentifier { get; set; }
    public string Specialization { get; set; }
    public string Profile { get; set; }
    public string UserId { get; set; }
    public ApplicationUser? User { get; set; }
}