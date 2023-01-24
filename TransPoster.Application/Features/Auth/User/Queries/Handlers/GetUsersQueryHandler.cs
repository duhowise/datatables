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
    private readonly IMapper _mapper;
    public GetUsersQueryHandler(IRepositoryAsync<ApplicationUser> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<ApplicationUserResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var spec = new ApplicationUserFilterSpecification(query.Request);
        var data = await _mapper.ProjectTo<ApplicationUserResponse>(_repository.Entities.Specify(spec))
            .ToPaginatedListAsync(query.Request.PageNumber, query.Request.PageSize);
        return data;
    }
}