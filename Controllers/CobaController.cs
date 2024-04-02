using Microsoft.AspNetCore.Mvc;
using myFirstWeb.Models;
using System.Diagnostics;

namespace myFirstWeb.Controllers
{
    public class CobaController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CobaController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
