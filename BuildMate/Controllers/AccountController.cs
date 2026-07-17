using Microsoft.AspNetCore.Mvc;

namespace Buildmate.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, bool rememberMe)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            TempData["ResetSent"] = "true";
            return RedirectToAction("ForgotPassword");
        }
    }
}