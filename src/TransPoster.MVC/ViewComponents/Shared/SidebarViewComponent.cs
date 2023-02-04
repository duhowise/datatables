using Microsoft.AspNetCore.Mvc;
using TransPoster.MVC.Controllers;

namespace TransPoster.MVC.ViewComponents.Shared;

public class SidebarViewComponent : ViewComponent
{
    private readonly ILogger<SidebarViewComponent> _logger;

    private readonly List<NavItem> _navItems = new()
        {
            new NavItem("Roles", "clipboard", nameof(RolesController), nameof(RolesController.Index)),
        };

    private readonly List<NavHead> _navHeads = new()
    {
        new NavHead("Users", "people", new()
        {
            new NavItem("All Users", "people-fill", nameof(UsersController), nameof(UsersController.Index)),
            new NavItem("Create User", "person-fill-add", nameof(UsersController), nameof(UsersController.Create)),
        })
    };

    public SidebarViewComponent(ILogger<SidebarViewComponent> logger) => _logger = logger;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewData["NavItems"] = _navItems;
        ViewData["NavHeads"] = _navHeads;

        return View();
    }
}

public record NavItem(string Name, string Icon, string ControllerName, string Action)
{
    public string Controller => ControllerName.Replace("Controller", "");
}

public record NavHead(string Name, string Icon, List<NavItem> NavItems);
