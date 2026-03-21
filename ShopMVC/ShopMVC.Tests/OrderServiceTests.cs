using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;
using ShopMVC.Services;
using Xunit;

namespace ShopMVC.Tests
{


    public class OrderServiceTests
    {
        private ShopDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public void PlaceOrder_ShouldCreateOrder()
        {
            using var ctx = CreateContext();
            var productService = new ProductService(ctx);
            var orderService = new OrderService(ctx);

            productService.Add(new Product { Name = "Apple", Price = 1m, Stock = 10, Category = "Fruit" });

            orderService.PlaceOrder("Ivan", new Dictionary<int, int> { { 1, 2 } });

            Assert.Single(orderService.GetAll());
        }

        [Fact]
        public void PlaceOrder_ShouldReduceStock()
        {
            using var ctx = CreateContext();
            var productService = new ProductService(ctx);
            var orderService = new OrderService(ctx);

            productService.Add(new Product { Name = "Apple", Price = 1m, Stock = 10, Category = "Fruit" });

            orderService.PlaceOrder("Ivan", new Dictionary<int, int> { { 1, 3 } });

            Assert.Equal(7, productService.GetById(1).Stock);
        }

        [Fact]
        public void PlaceOrder_InsufficientStock_ShouldThrow()
        {
            using var ctx = CreateContext();
            var productService = new ProductService(ctx);
            var orderService = new OrderService(ctx);

            productService.Add(new Product { Name = "Apple", Price = 1m, Stock = 2, Category = "Fruit" });

            Assert.Throws<InvalidOperationException>(() =>
                orderService.PlaceOrder("Ivan", new Dictionary<int, int> { { 1, 5 } }));
        }

        [Fact]
        public void Delete_Order_ShouldRemoveIt()
        {
            using var ctx = CreateContext();
            var productService = new ProductService(ctx);
            var orderService = new OrderService(ctx);

            productService.Add(new Product { Name = "Apple", Price = 1m, Stock = 10, Category = "Fruit" });
            orderService.PlaceOrder("Ivan", new Dictionary<int, int> { { 1, 1 } });

            orderService.Delete(1);

            Assert.Empty(orderService.GetAll());
        }
    }
}