using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransPorter.Shared.Enums;
using TransPoster.Application.Features.Auth.RegisterUser.Command;
using TransPoster.Application.Features.Auth.User.Commands;
using TransPoster.Application.Features.Auth.User.DTOs.Request;
using TransPoster.Application.Features.Auth.User.DTOs.Response;
using TransPoster.Application.Features.Auth.User.Queries;
using TransPoster.MVC.Models;

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

    [HttpPost("dtusers")]
    public async Task<IActionResult> Get([FromBody] DtParameters body)
    {
        var searchBy = body.Search?.Value;
        var start = body.Start;
        var size = body.Length;

        var page = start;

        // now just get the count of items (without the skip and take) - eg how many could be returned with filtering

        var result = await _mediator.Send(new GetUsersQuery(new GetPagedUsersRequest()
        {
            PageNumber = start,
            PageSize = size,
            SortDirection = SortDirection.None
        }));

        return new JsonResult(new DtResult<ApplicationUserResponse>
        {
            Draw = body.Draw,
            RecordsTotal = result.TotalCount,
            RecordsFiltered = result.TotalCount,
            Data = result.Data,
        });
    }
}
