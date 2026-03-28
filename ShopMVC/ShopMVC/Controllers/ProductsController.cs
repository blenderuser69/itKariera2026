using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using ShopMVC.Services;

namespace ShopMVC.Controllers
{
    //crud operations for the products only for admin
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("IsLoggedIn") == "true";
        }

        //shows all products with search option
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

        //shows the form for adding a new product
        public IActionResult Create()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        //saves the new product and returns error if product already exists
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }

            bool added = _productService.Add(product);

            if (!added)
            {
                ViewBag.Error = $"Product '{product.Name}' already exists!";
                return View(product);
            }

            return RedirectToAction("Index");
        }
        //shows the form for editing a product
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

        //saves the changes to the product
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

        //shows confirmation before deleting
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

        //removes the product from the database
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