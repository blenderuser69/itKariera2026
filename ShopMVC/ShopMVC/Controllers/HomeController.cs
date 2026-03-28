using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using System.Diagnostics;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        // /home
        public IActionResult Index()
        {
            return View();
        }
        //page for erros
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
