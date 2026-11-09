using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // LOGIN

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _apiService.LoginAsync(model);

            if (user == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid email address or password.");

                return View(model);
            }

            HttpContext.Session.SetInt32(
                "UserAccountID", user.UserAccountID);

            HttpContext.Session.SetString(
                "FirstName", user.FirstName);

            HttpContext.Session.SetString(
                "LastName", user.LastName);

            HttpContext.Session.SetString(
                "Email", user.Email);

            HttpContext.Session.SetString(
                "Role", user.Role);

            return RedirectToDashboard(user.Role);
        }

        // REGISTRATION

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _apiService.RegisterAsync(model);

            if (user == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Registration failed. Please check your details or try another email.");

                return View(model);
            }

            HttpContext.Session.SetInt32(
                "UserAccountID", user.UserAccountID);

            HttpContext.Session.SetString(
                "FirstName", user.FirstName);

            HttpContext.Session.SetString(
                "LastName", user.LastName);

            HttpContext.Session.SetString(
                "Email", user.Email);

            HttpContext.Session.SetString(
                "Role", user.Role);

            return RedirectToDashboard(user.Role);
        }

        // ROLE-BASED DASHBOARD REDIRECTION

        private IActionResult RedirectToDashboard(string role)
        {
            if (role == "Participant")
            {
                return RedirectToAction("Participant", "Dashboard");
            }

            if (role == "Organizer")
            {
                return RedirectToAction("Organizer", "Dashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        // LOGOUT

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}