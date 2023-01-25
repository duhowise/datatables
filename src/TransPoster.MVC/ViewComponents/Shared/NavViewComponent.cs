using Microsoft.AspNetCore.Mvc;
using TransPoster.MVC.Controllers;

namespace TransPoster.MVC.ViewComponents.Shared;

public class NavViewComponent : ViewComponent
{
    private readonly ILogger<NavViewComponent> _logger;


    public NavViewComponent(ILogger<NavViewComponent> logger) => _logger = logger;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        _logger.LogInformation("These are cool stuff we are doing here");

        return View();
    }
}
