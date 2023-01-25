using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TransPorter.Shared.Interfaces;

namespace TransPorter.Shared.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
        var userIdString = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdString, out var userId))
            UserId = userId;
        Claims = httpContextAccessor.HttpContext?.User.Claims.AsEnumerable()
            .Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList() ?? new List<KeyValuePair<string, string>>();
       // UserName = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.Email)?.Value;
    }

    public Guid UserId { get; }
    public List<KeyValuePair<string, string>> Claims { get; }

    public bool IsInRole(string role)
    {
        if (_contextAccessor.HttpContext == null) return false;
        return _contextAccessor.HttpContext.User.IsInRole(role);
    }
}
