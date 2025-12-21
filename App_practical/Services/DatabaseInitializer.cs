using _4_Calculator.Data;
using _4_Calculator.Models;

namespace _4_Calculator.Services
{
    public class DatabaseInitializer
    {
        private readonly TemperatureContext _context;
        private readonly Random _random = new Random();

        public DatabaseInitializer(TemperatureContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Check if we already have data
            if (!_context.TemperatureData.Any())
            {
                await SeedDataAsync();
            }
        }

        private async Task SeedDataAsync()
        {
            var temperatureDataList = new List<TemperatureData>();
            var baseTime = new TimeSpan(12, 0, 0); // 12:00:00
            var baseDate = DateTime.Today;

            for (int i = 0; i < 100; i++)
            {
                var temperature = Math.Round(_random.NextDouble() * (100 - 30) + 30, 1); // Random temp 30-100
                var time = baseTime.Add(TimeSpan.FromMinutes(i)); // Add i minutes
                var fullDateTime = baseDate.Add(time);

                temperatureDataList.Add(new TemperatureData
                {
                    Temperature = temperature,
                    Time = time,
                    FullDateTime = fullDateTime
                });
            }

            await _context.TemperatureData.AddRangeAsync(temperatureDataList);
            await _context.SaveChangesAsync();
        }
    }
}
