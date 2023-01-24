using Microsoft.AspNetCore.Mvc;
using TransPoster.MVC.Controllers;

namespace TransPoster.MVC.ViewComponents.Shared;

public class NavViewComponent : ViewComponent
{
    private readonly ILogger<NavViewComponent> _logger;

    private readonly List<NavItem> _navItems = new()
    {
        new NavItem("Users", "users", nameof(UsersController), nameof(UsersController.Index)),
        new NavItem("Roles", "clipboard", nameof(RolesController), nameof(RolesController.Index)),
    };

    public NavViewComponent(ILogger<NavViewComponent> logger) => _logger = logger;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        _logger.LogInformation("These are the items: ", _navItems);

        return View(_navItems);
    }
}

public record NavItem(string Name, string Icon, string ControllerName, string Action)
{
    public string Controller => ControllerName.Replace("Controller", "");
}