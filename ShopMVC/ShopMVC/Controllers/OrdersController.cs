using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using ShopMVC.Services;

namespace ShopMVC.Controllers
{
    
    
    /// crud operations for /Orders.
    
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;

        public OrdersController(OrderService orderService, ProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        //shows all orders
        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        //shows the form for placing a new order
        public IActionResult Create()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        //saves the new order with selected products and quantities
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

        //shows confirmation before deleting the order
        public IActionResult Delete(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //removes the order from the database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction("Index");
        }
        //updates the status of an order - Pending, Completed, Cancelled
        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            _orderService.UpdateStatus(id, newStatus);
            return RedirectToAction("Index");
        }
    }
}