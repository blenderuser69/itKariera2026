using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;

namespace ShopMVC.Services
{
    public class OrderService
    {
        private readonly ShopDbContext _context;

        public OrderService(ShopDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
            => _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList();

        public Order? GetById(int id)
            => _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id);

        public void PlaceOrder(string customerName, Dictionary<int, int> productQuantities)
        {
            var order = new Order { CustomerName = customerName };

            foreach (var (productId, qty) in productQuantities)
            {
                var product = _context.Products.Find(productId);
                if (product == null || product.Stock < qty)
                    throw new InvalidOperationException($"Product {productId} unavailable.");

                product.Stock -= qty;
                order.Items.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = qty,
                    UnitPrice = product.Price
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public bool UpdateStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null) return false;

            order.Status = newStatus;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }
    }
}