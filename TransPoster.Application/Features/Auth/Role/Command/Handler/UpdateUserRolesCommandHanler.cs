using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.RegisterUser.Command.Handler;
using TransPoster.Application.Features.Auth.Role.DTOs.Request;

namespace TransPoster.Application.Features.Auth.Role.Command.Handler;

public class UpdateUserRolesCommandHanler : IRequestHandler<UpdateUserRolesCommand, IResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<RegisterUserCommandHandler> _localizer;
    public UpdateUserRolesCommandHanler(UserManager<ApplicationUser> userManager, IStringLocalizer<RegisterUserCommandHandler> localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
    }
    public async Task<IResult> Handle(UpdateUserRolesCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.Request.UserId.ToString());
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        result = await _userManager.AddToRolesAsync(user, command.Request.UserRoles.Where(x => UserRoleModel.Selected).Select(y => y.RoleName));
        return await Result.SuccessAsync(_localizer[$"Role(s) updated for {user.FirstName} - {user.Email}"]);
    }
}

