using casestudy2.Data;
using casestudy2.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace casestudy2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
            builder.Services.AddScoped<ICartRespositories, CartRespositories>();
            builder.Services.AddScoped<IOrderRespositories, OrderRespositories>();
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    string? connectionString = builder.Configuration.GetConnectionString("ApplicationDBContext");
                    options.UseSqlServer(connectionString);
                }
            );

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("");
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=HomePage}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}
