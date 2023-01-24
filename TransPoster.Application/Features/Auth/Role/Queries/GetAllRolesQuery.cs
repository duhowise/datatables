using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransPorter.Domain;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.Role.DTOs.Response;

namespace TransPoster.Application.Features.Auth.Role.Queries;

public class GetAllRolesQuery : IRequest<Result<List<RoleResponse>>> { }

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<RoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;
    public GetAllRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    { _roleManager = roleManager; _mapper = mapper; }
    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);        
        var mappedRoles = _mapper.Map<List<RoleResponse>>(roles);
        return await Result<List<RoleResponse>>.SuccessAsync(mappedRoles);
    }
}