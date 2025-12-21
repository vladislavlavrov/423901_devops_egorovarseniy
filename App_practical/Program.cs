using Microsoft.EntityFrameworkCore;
using _4_Calculator.Data;
using _4_Calculator.Services;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TemperatureContext>(options => options.UseSqlite("Data Source=temperature.db"));
            builder.Services.AddScoped<DatabaseInitializer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            async Task InitializeDatabase()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                    await dbInitializer.InitializeAsync();
                }
            }
            InitializeDatabase().GetAwaiter().GetResult();
            app.Run();
        }
    }
}
