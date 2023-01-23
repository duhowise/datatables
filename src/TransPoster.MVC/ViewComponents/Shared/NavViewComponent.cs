using Microsoft.AspNetCore.Mvc;

namespace TransPoster.MVC.ViewComponents.Shared;

public class NavViewComponent : ViewComponent
{
    private readonly ILogger<NavViewComponent> _logger;

    public NavViewComponent(ILogger<NavViewComponent> logger) => _logger = logger;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<string> items = new()
        {
            "Hello",
            "Hi",
            "Face"
        };

        _logger.LogInformation("These are the items: ", items);

        return View(items);
    }
}