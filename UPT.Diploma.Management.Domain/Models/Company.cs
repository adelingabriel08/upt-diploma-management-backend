using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Domain.Models;

public class Company : EntityBase
{
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string ShortDescription { get; set; }
    public string UserId { get; set; }
    public ApplicationUser? User { get; set; }
}