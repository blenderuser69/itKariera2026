using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using ShopMVC.Services;

namespace ShopMVC.Controllers
{
    
    /// Controller za upravlenie na poruchki
    /// obrabotva vsichki zaqvki kum /Orders.
    
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;

        public OrdersController(OrderService orderService, ProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        //pokazva vsichki poruchki
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        //pokazva formata za poruchka
        public IActionResult Create()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        //zapisvane na poruchkata
        [HttpPost]
        public IActionResult Create(string customerName, List<int> productIds, List<int> quantities)
        {
            var productQuantities = new Dictionary<int, int>();

            for (int i = 0; i < productIds.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    productQuantities[productIds[i]] = quantities[i];
                }
            }

            if (productQuantities.Count == 0)
            {
                ViewBag.Error = "Please select at least one product!";
                return View(_productService.GetAll());
            }

            try
            {
                _orderService.PlaceOrder(customerName, productQuantities);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(_productService.GetAll());
            }
        }

        //potvurjdava iztrivaneto
        public IActionResult Delete(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //iztriva poruchkata
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}