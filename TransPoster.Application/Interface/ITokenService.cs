using System.Security.Claims;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Token.DTOs;

namespace TransPoster.Application.Interface;

public interface ITokenService
{
    Task<string> GenerateJwtAsync(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<IResult<TokenResponse>> GetRefreshToken(ApplicationUser user);
}
