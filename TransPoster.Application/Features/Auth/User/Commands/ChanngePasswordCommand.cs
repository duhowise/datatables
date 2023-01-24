using MediatR;
using System.ComponentModel.DataAnnotations;
using TransPorter.Shared.Wrapper;

namespace TransPoster.Application.Features.Auth.User.Commands;

public class ChanngePasswordCommand : IRequest<IResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string CurrentPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = null!;
}
