using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;
using ShopMVC.Services;
using Xunit;

namespace ShopMVC.Tests
{

    public class ProductServiceTests
    {
        private ShopDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public void Add_ShouldIncreaseProductCount()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);

            service.Add(new Product { Name = "Test", Price = 1.0m, Stock = 10, Category = "Test" });

            Assert.Single(service.GetAll());
        }

        [Fact]
        public void Add_MultipleProducts_ShouldReturnAll()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);

            service.Add(new Product { Name = "A", Price = 1m, Stock = 1, Category = "X" });
            service.Add(new Product { Name = "B", Price = 2m, Stock = 2, Category = "Y" });
            service.Add(new Product { Name = "C", Price = 3m, Stock = 3, Category = "Z" });

            Assert.Equal(3, service.GetAll().Count);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectProduct()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "Milk", Price = 1.5m, Stock = 20, Category = "Dairy" });

            var product = service.GetById(1);

            Assert.NotNull(product);
            Assert.Equal("Milk", product.Name);
        }

        [Fact]
        public void GetAll_EmptyDatabase_ShouldReturnEmptyList()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);

            var result = service.GetAll();

            Assert.Empty(result);
        }
    }
}