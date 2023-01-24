using MediatR;
using TransPorter.Shared.Wrapper;

namespace TransPoster.Application.Features.Auth.Role.Command;

public struct RegisterRoleCommand : IRequest<IResult<Guid>>
{
    public string RoleName { get; set; }
    public List<string> Permissions { get; set; }
}
