using _4_Calculator.Data;
using _4_Calculator.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Confluent.Kafka;
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

            string mariadbCS = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CalculatorContext>(options =>
            {
                options.UseMySql(mariadbCS, new MySqlServerVersion(new
                Version(10, 5, 15)));
            });
            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient();

            builder.Services.AddHostedService<KafkaConsumerService>();
            builder.Services.AddSingleton<KafkaProducerHandler>();
            builder.Services.AddSingleton<KafkaProducerService<Null, string>>();

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

            app.Run();
        }
    }
}
