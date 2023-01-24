using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;

namespace TransPoster.Application.Features.Auth.RegisterUser.Command.Handler;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult<Guid>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<RegisterUserCommandHandler> _localizer;
    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<RegisterUserCommandHandler> localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
    }

    public async Task<IResult<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser(command.FirstName, command.Surname, command.Gender, command.Email);
        var result = await _userManager.CreateAsync(user, command.Password);
        if (result.Succeeded) return await Result<Guid>.SuccessAsync(user.Id,
            _localizer[$"{user}'s, account created successfully done. Activating link has been sent the email {user.Email}"]);
        var errors = result.Errors.Select(identityError => _localizer[identityError.Description].ToString()).ToList();
        return await Result<Guid>.FailAsync(errors);
    }
}
