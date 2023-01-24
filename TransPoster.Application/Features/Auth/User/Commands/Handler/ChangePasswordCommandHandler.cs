using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;

namespace TransPoster.Application.Features.Auth.User.Commands.Handler;

public class ChangePasswordCommandHandler : IRequestHandler<ChanngePasswordCommand, IResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<ChangePasswordCommandHandler> _localizer;
    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<ChangePasswordCommandHandler> localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
    }

    public async Task<IResult> Handle(ChanngePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        // Don't reveal that the user does not exist
        if (user == null) return await Result.FailAsync(_localizer["Email/user name doesn't exists!"]);
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded) return await Result.SuccessAsync(_localizer["Password changed Successfully!"]);
        var errors = result.Errors.Select(identityError => identityError.Description).ToList();
        return await Result.FailAsync(errors);
    }
}

