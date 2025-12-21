using Microsoft.EntityFrameworkCore;
using _4_Calculator.Models;

namespace _4_Calculator.Data
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        {
        }

        public DbSet<TemperatureData> TemperatureData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureData>()
                .Property(t => t.Time)
                .HasConversion(
                    v => v.ToString(@"hh\:mm\:ss"),
                    v => TimeSpan.Parse(v));
        }
    }
}
