using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TransPorter.Domain.Shared;
using TransPorter.Shared.Enums;

namespace TransPorter.Domain;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    [MaxLength(100), Required]
    public string FirstName { get; set; } = null!;
    [MaxLength(100), Required]
    public string LastName { get; set;} = null!;
    [Required]
    public Gender Gender { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? ModifiedUserId { get; set; }
    public DateTime? ModifiedOn { get; set; }
}