namespace TransPoster.Application.Features.Auth.Role.DTOs.Request;

public class UpdateUserRolesRequest
{
    public long UserId { get; set; }
    public IList<UserRoleModel> UserRoles { get; set; } = null!;
}

public class UserRoleModel
{
    public string RoleName { get; set; } = null!;
    public static bool Selected => true;
}