using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TransPorter.Shared.Enums;
using TransPorter.Shared.Interfaces;

namespace TransPorter.Domain;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    [MaxLength(100), Required]
    public string FirstName { get; set; } = null!;
    [MaxLength(100), Required]
    public string LastName { get; set;} = null!;
    [Required]
    public Gender Gender { get; set; }
    public string? RefreshToken { get; set;}
    public DateTime RefreshTokenExpiryTime { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? ModifiedUserId { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsActive { get; set; }
    public ApplicationUser() { }

    public ApplicationUser(string firstName, string lastName, Gender gender, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
    }
}