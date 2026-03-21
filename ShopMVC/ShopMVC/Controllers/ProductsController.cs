using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using ShopMVC.Services;

namespace ShopMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // Помощен метод - проверява дали е влязъл
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("IsLoggedIn") == "true";
        }

        //pokazva spisuk s vsichi producti
        public IActionResult Index(string search)
        {
            ViewBag.Search = search;
            ViewBag.IsLoggedIn = IsLoggedIn();

            if (string.IsNullOrEmpty(search))
            {
                var products = _productService.GetAll();
                return View(products);
            }
            else
            {
                var products = _productService.Search(search);
                return View(products);
            }
        }

        //pokazva forma na dobavqne
        public IActionResult Create()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            _productService.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        //zapisva promenite
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        //potvurjdenie za iztrivane
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        //triene na producta
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}