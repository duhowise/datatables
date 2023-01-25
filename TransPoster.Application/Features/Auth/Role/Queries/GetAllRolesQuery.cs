using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Role.DTOs.Response;
using System.Linq;

namespace TransPoster.Application.Features.Auth.Role.Queries;

public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>> { }

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    public GetAllRolesQueryHandler(RoleManager<ApplicationRole> roleManager)
    { _roleManager = roleManager;}
    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);        
        var mappedRoles = (from role in roles
                           let roleResponse = new RoleResponse { RoleId = role.Id, RoleName = role.Name ?? string.Empty }
                           select roleResponse).ToList();
        return await Result<List<RoleResponse>>.SuccessAsync(mappedRoles);
    }
}