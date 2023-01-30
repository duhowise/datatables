using Microsoft.AspNetCore.Mvc;
using TransPorter.Shared.Enums;
using TransPoster.Application.Features.Auth.RegisterUser.Command;
using TransPoster.MVC.Models.Users;

namespace TransPoster.MVC.Controllers;

public class UsersController : BaseController<UsersController>
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }

    public IActionResult Show()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Store(CreateUser body)
    {
        var result = await _mediator.Send(new RegisterUserCommand()
        {
            Email = body.Email,
            Password = body.Password,
            FirstName = body.FirstName,
            Surname = body.Surname,
            PhoneNumber = body.PhoneNumber,
            Gender = body.Gender is "male" ? Gender.Male : Gender.Female
        });

        _logger.LogInformation("Hi, {Result}", result);

        return RedirectToAction(actionName: "Index", controllerName: "Users");
    }
}