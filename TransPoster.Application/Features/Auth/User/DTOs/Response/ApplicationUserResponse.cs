using TransPorter.Shared.Enums;

namespace TransPoster.Application.Features.Auth.User.DTOs.Response;

public class ApplicationUserResponse 
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!; 
    public Gender Gender { get; set; }
}
