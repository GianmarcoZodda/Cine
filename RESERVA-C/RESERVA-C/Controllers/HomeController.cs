using Microsoft.AspNetCore.Mvc;
using RESERVA_C.Models;
using System.Diagnostics;

namespace RESERVA_C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? mensaje)
        {
            ViewBag.Mensaje = mensaje;  

            return View();
        }

        public IActionResult InformacionInstitucional()
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