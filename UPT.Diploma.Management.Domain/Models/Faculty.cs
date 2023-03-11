using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Domain.Models;

public class Faculty : EntityBase
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    
    public ICollection<ApplicationUser>? Users { get; set; }
}