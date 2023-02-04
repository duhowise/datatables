using MediatR;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Token.DTOs;

namespace TransPoster.Application.Features.Auth.Token.Command;

public struct AuthenticationCommand : IRequest<IResult>
{
    public string Email { get; set; } 
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
