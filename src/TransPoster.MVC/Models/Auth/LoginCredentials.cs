using System.ComponentModel.DataAnnotations;

namespace TransPoster.MVC.Models.Auth;

public class LoginCredentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(7)]
    [RegularExpression("^[a-zA-Z0-9]*$")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

