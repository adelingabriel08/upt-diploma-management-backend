using Microsoft.AspNetCore.Identity;

namespace UPT.Diploma.Management.Domain.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int? FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
    public Student? Student { get; set; }
    public Company? Company { get; set; }
    public ICollection<Topic>? Topics { get; set; }
    public ICollection<Application>? Applications { get; set; }
}