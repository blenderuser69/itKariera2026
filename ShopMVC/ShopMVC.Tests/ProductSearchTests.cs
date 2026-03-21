using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;
using ShopMVC.Services;
using Xunit;

namespace ShopMVC.Tests
{

    public class ProductSearchTests
    {
        private ShopDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public void Search_ShouldFindByName()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "Apple Juice", Price = 2m, Stock = 10, Category = "Drinks" });
            service.Add(new Product { Name = "Milk", Price = 1m, Stock = 5, Category = "Dairy" });

            var results = service.Search("Apple");

            Assert.Single(results);
        }

        [Fact]
        public void Search_ShouldFindByCategory()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "Milk", Price = 1m, Stock = 5, Category = "Dairy" });
            service.Add(new Product { Name = "Cheese", Price = 2m, Stock = 10, Category = "Dairy" });
            service.Add(new Product { Name = "Apple", Price = 0.5m, Stock = 20, Category = "Fruit" });

            var results = service.Search("Dairy");

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void Search_NoResults_ShouldReturnEmpty()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "Milk", Price = 1m, Stock = 5, Category = "Dairy" });

            var results = service.Search("XYZ");

            Assert.Empty(results);
        }
    }
}