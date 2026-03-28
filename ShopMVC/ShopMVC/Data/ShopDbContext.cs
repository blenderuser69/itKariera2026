using Microsoft.EntityFrameworkCore;
using ShopMVC.Models;

namespace ShopMVC.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //the constructor gets the settings from program.cs
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple", Price = 0.99m, Stock = 100, Category = "Fruit" },
                new Product { Id = 2, Name = "Milk", Price = 1.49m, Stock = 50, Category = "Dairy" },
                new Product { Id = 3, Name = "Bread", Price = 2.00m, Stock = 30, Category = "Bakery" }
            );
        }
    }
}