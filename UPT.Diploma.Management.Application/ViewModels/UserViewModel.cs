
namespace UPT.Diploma.Management.Application.ViewModels;

public record UserViewModel
{
    public string Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Email { get; set; }
    public int? FacultyId { get; set; }
}