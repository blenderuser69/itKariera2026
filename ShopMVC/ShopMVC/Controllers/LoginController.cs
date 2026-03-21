using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToAction("Index", "Products");
            }

            ViewBag.Error = "Invalid username or password!";
            return View();
        }

        // GET: /Login/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Products");
        }
    }
}