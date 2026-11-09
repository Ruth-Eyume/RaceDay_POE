using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;


namespace RaceDayMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Participant()
        {
            var userId = HttpContext.Session.GetString("UserAccountID");
            var role = HttpContext.Session.GetString("Role");

            if (userId == null || role != "Participant")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        private readonly ApiService _apiService;

        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Organizer()
        {
            var userId = HttpContext.Session.GetString("UserAccountID");
            var role = HttpContext.Session.GetString("Role");

            if (userId == null || role != "Organizer")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DiscoverEvents()
        {
            try
            {
                var events = await _apiService.GetEventsAsync();

                return View(events);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Unable to load events. Please try again later.";

                return View(new List<EventViewModel>());
            }
        }
    }
}