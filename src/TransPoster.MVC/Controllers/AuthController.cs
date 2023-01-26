using Microsoft.AspNetCore.Mvc;
using TransPoster.Application.Features.Auth.Token.Command;
using TransPoster.MVC.Models.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TransPoster.MVC.Controllers;

public class AuthController : BaseController<AuthController>
{
    private const string LoginAttemptsSessionName = "_LoginAttempts";

    public IActionResult Login()
    {
        ViewData["loginAttempts"] = HttpContext.Session.GetInt32(LoginAttemptsSessionName) ?? 0;
        ViewData["LoginText"] = _localizer["Login"].Value;
        _logger.LogInformation("This is the login credentials {Login}", _localizer["Login"].Value);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitLogin(LoginCredentials body)
    {
        _logger.LogInformation("This is the login credentials {Email}, {Password}, {RememberMe}", body.Email, body.Password, body.RememberMe);

        var result = await _mediator.Send(new AuthenticationCommand()
        {
            Email = body.Email,
            Password = body.Password,
            RememberMe = body.RememberMe,
        });

        _logger.LogInformation("ddjfjkd {Res}", result);

        // for failed attempts
        var loginAttempts = HttpContext.Session.GetInt32(LoginAttemptsSessionName) ?? 0;
        HttpContext.Session.SetInt32(LoginAttemptsSessionName, loginAttempts + 1);
        _logger.LogInformation("Session key: {sessionKey}", loginAttempts + 1);

        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }
}