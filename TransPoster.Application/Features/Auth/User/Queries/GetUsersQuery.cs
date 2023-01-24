using MediatR;
using TransPorter.Shared.Wrapper;
using TransPoster.Application.Features.Auth.User.DTOs.Request;
using TransPoster.Application.Features.Auth.User.DTOs.Response;

namespace TransPoster.Application.Features.Auth.User.Queries;

public class GetUsersQuery : IRequest<PaginatedResult<ApplicationUserResponse>>
{
    public GetPagedUsersRequest Request { get; }
    public GetUsersQuery(GetPagedUsersRequest request)
    {
        Request = request;
    }
}
