using Microsoft.EntityFrameworkCore;
using ShopMVC.Data;
using ShopMVC.Services;

namespace ShopMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // registrira baza danni s sqlLite
            builder.Services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlite("Data Source=shop.db"));

            // nova instanciq za vsqka zaqvka
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<OrderService>();

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseStaticFiles();
            //opredelq koy controler koq zaqvka
            app.UseRouting();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}