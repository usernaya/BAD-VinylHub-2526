using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Web.Models;

namespace VinylHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVinylService _vinylService;

        public HomeController(IVinylService vinylService)
        {
            _vinylService = vinylService;
        }

        public IActionResult Index()
        {
            var recentVinyls = _vinylService.GetAll().OrderByDescending(v => v.Id).Take(4).ToList();
            ViewBag.RecentVinyls = recentVinyls;
            ViewBag.TotalVinyls = _vinylService.GetAll().Count();
            ViewBag.AvailableVinyls = _vinylService.GetAll().Count(v => v.IsAvailable);
            return View();
        }
    }
}
