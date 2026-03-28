using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Models;

namespace ShopMVC.Services
{
    public class ProductService
    {
        private readonly ShopDbContext _context;

        public ProductService(ShopDbContext context)
        {
            _context = context;
        }
        //takes all the products
        public List<Product> GetAll()
            => _context.Products.ToList();
        //searches by id
        public Product GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public bool Add(Product product)
        {
            
            bool exists = _context.Products
                .Any(p => p.Name.ToLower() == product.Name.ToLower());

            if (exists)
            {
                return false;
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }


        public bool Update(Product updated)
        {
            var product = _context.Products.Find(updated.Id);
            if (product == null) return false;

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            product.Category = updated.Category;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public List<Product> Search(string keyword)
            => _context.Products
                .Where(p => p.Name.Contains(keyword) || p.Category.Contains(keyword))
                .ToList();
    }
}