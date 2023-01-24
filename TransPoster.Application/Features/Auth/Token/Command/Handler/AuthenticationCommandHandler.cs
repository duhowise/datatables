using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Token.DTOs;
using TransPoster.Application.Interface;

namespace TransPoster.Application.Features.Auth.Token.Command.Handler
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, IResult<TokenResponse>>
    {
        private static readonly string ErrowMessage = "Invalid Login Attempt!!!";
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<AuthenticationCommandHandler> _localizer;

        public AuthenticationCommandHandler(ITokenService tokenService, SignInManager<ApplicationUser> signInManager,
        IStringLocalizer<AuthenticationCommandHandler> localizer, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult<TokenResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) return await Result<TokenResponse>.FailAsync(_localizer[ErrowMessage]);
            if (!user.IsActive)
                return await Result<TokenResponse>.FailAsync(_localizer["User Not Active. Please contact the administrator."]);
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password,
                   request.RememberMe, true);
            if (result.Succeeded) return await _tokenService.GetRefreshToken(user);
            if (result.IsLockedOut) return await Result<TokenResponse>.FailAsync(_localizer["Access Denied. Account Blocked"]);
            if (result.IsNotAllowed) return await Result<TokenResponse>.FailAsync(_localizer["Access Denied. Authentication not allowed."]);
            return await Result<TokenResponse>.FailAsync(_localizer[ErrowMessage]);
        }
    }

    
}
