using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;
using ShopMVC.Services;
using Xunit;

namespace ShopMVC.Tests
{

    public class ProductUpdateDeleteTests
    {
        private ShopDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ShopDbContext(options);
        }

        [Fact]
        public void Update_ShouldChangeName()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "Old", Price = 1m, Stock = 5, Category = "X" });

            var product = service.GetById(1);
            product.Name = "New";
            service.Update(product);

            Assert.Equal("New", service.GetById(1).Name);
        }

        [Fact]
        public void Update_NonExistent_ShouldReturnFalse()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);

            bool result = service.Update(new Product { Id = 999, Name = "X", Price = 1m, Stock = 1, Category = "X" });

            Assert.False(result);
        }

        [Fact]
        public void Delete_ShouldRemoveProduct()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);
            service.Add(new Product { Name = "ToDelete", Price = 1m, Stock = 1, Category = "X" });

            service.Delete(1);

            Assert.Empty(service.GetAll());
        }

        [Fact]
        public void Delete_NonExistent_ShouldReturnFalse()
        {
            using var ctx = CreateContext();
            var service = new ProductService(ctx);

            bool result = service.Delete(999);

            Assert.False(result);
        }
    }
}