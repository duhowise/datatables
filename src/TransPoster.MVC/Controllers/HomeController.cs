using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace TransPoster.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<AuthController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }


        public IActionResult Index()
        {
            ViewData["WelcomeText"] = _localizer["Welcome"].Value;
            ViewData["LearnText"] = _localizer["Learn"].Value;

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
