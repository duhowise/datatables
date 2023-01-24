using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using System.Transactions;
using TransPorter.Domain;
using TransPorter.Shared.Interfaces;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.RegisterUser.Command.Handler;

namespace TransPoster.Application.Features.Auth.Role.Command.Handler;

public class RefisterRoleCommandHandler : IRequestHandler<RegisterRoleCommand, IResult<Guid>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IStringLocalizer<RegisterUserCommandHandler> _localizer;
    private readonly ICurrentUserService _currentUserService;
    public RefisterRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IStringLocalizer<RegisterUserCommandHandler> localizer,
       ICurrentUserService currentUserService)
    {
        _roleManager = roleManager;
        _localizer = localizer;
        _currentUserService = currentUserService;
    }

    public async Task<IResult<Guid>> Handle(RegisterRoleCommand command, CancellationToken cancellationToken)
    {
        var role = new ApplicationRole { Name = command.RoleName, CreatedOn = DateTime.UtcNow, CreatedUserId = _currentUserService.UserId };
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await _roleManager.CreateAsync(role);
        foreach (var permission in command.Permissions)
        {
            var claim = new Claim("Permission", permission);
            await _roleManager.AddClaimAsync(role, claim);
        }
        scope.Complete();
        if (result.Succeeded) return await Result<Guid>.SuccessAsync(role.Id, _localizer["User group (role) creation successfully done."]);
        var errors = result.Errors.Select(identityError => identityError.Description).ToList();
        return await Result<Guid>.FailAsync(errors);
    }
}
