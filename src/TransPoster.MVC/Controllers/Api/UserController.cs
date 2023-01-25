using Microsoft.AspNetCore.Mvc;
using TransPoster.Application.Features.Auth.RegisterUser.Command;
using TransPoster.Application.Features.Auth.User.Commands;
using TransPoster.Application.Features.Auth.User.DTOs.Request;
using TransPoster.Application.Features.Auth.User.Queries;

namespace TransPoster.MVC.Controllers.Api;

public class UserController : BaseApiController<UserController>
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword(ChanngePasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Get(GetPagedUsersRequest request)
     => Ok(await _mediator.Send(new GetUsersQuery(request)));
}
