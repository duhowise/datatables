using Microsoft.AspNetCore.Mvc;
using TransPoster.Application.Features.Auth.Role.Command;
using TransPoster.Application.Features.Auth.Role.DTOs.Request;
using TransPoster.Application.Features.Auth.Role.Queries;

namespace TransPoster.MVC.Controllers.Api;

public class RoleController : BaseApiController<RoleController>
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRoleCommand command)
        => Ok(await _mediator.Send(command));

    /// <summary>
    /// Get all User Roles / Office groups
    /// </summary>
    /// <returns></returns> 
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetAllRolesQuery()));

    /// <summary>
    /// set, remove, update user roles
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesRequest request)
        => Ok(await _mediator.Send(new UpdateUserRolesCommand(request)));
}
