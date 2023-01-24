using Microsoft.AspNetCore.Mvc;

namespace TransPoster.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
