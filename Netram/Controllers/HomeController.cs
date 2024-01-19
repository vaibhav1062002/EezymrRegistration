using Microsoft.AspNetCore.Mvc;
using Netram.Models;
using System.Diagnostics;

namespace Netram.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           List<PackageDetailsEntity> list = new List<PackageDetailsEntity>();
           EazymrReposetory eazymrReposetory = new EazymrReposetory();
           list = eazymrReposetory.ShowSetPackage();
           ViewBag.PackageDetails = list;

			return View(list);
        }

        public IActionResult Privacy()
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
