using TransPorter.Shared.DTOs.Request;

namespace TransPoster.Application.Features.Auth.User.DTOs.Request
{
    public class GetPagedUsersRequest : PagedRequest
    {
        public Guid? RoleId { get; set; }
        public Guid? UserId { get; set; }
    }
}
