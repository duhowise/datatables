using MediatR;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Role.DTOs.Request;

namespace TransPoster.Application.Features.Auth.Role.Command;

public class UpdateUserRolesCommand : IRequest<IResult>
{
    public UpdateUserRolesRequest Request { get; private set; } = null!;
    public UpdateUserRolesCommand(UpdateUserRolesRequest request)
        => Request = request;
}
