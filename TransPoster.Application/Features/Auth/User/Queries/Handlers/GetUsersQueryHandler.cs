using AutoMapper;
using MediatR;
using TransPorter.Domain;
using TransPorter.Infrastructure.Extensions;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.User.DTOs.Response;
using TransPoster.Application.Features.Auth.User.Queries.Specifications;
using TransPoster.Application.Interface;

namespace TransPoster.Application.Features.Auth.User.Queries.Handlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResult<ApplicationUserResponse>>
{
    private readonly IRepositoryAsync<ApplicationUser> _repository;
    public GetUsersQueryHandler(IRepositoryAsync<ApplicationUser> repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<ApplicationUserResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var spec = new ApplicationUserFilterSpecification(query.Request);
        var data = await _repository.Entities.Specify(spec)
            .Select(u=> new ApplicationUserResponse
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender= u.Gender,
            })
            .ToPaginatedListAsync(query.Request.PageNumber, query.Request.PageSize);
        return data;
    }
}