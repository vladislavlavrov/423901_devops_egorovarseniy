using System.Diagnostics;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _4_Calculator.Data;
using _4_Calculator.Models;
using _4_Calculator.ViewModels;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TemperatureContext _context;

        public HomeController(ILogger<HomeController> logger, TemperatureContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(TemperatureFilterViewModel filter)
        {
            // Получаем все данные из базы
            var allData = await _context.TemperatureData
                .OrderBy(t => t.Time)
                .ToListAsync();

            // Получаем уникальные времена (на клиенте, после загрузки данных)
            var allTimes = allData
                .Select(t => t.Time.ToString(@"hh\:mm\:ss"))
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            // Подготавливаем модель фильтра с доступными временами
            filter.AvailableTimes = allTimes;

            // Фильтруем данные
            var filteredData = allData.AsQueryable();

            // Применяем фильтры, если указаны
            if (!string.IsNullOrEmpty(filter.StartTime) && TimeSpan.TryParse(filter.StartTime, out var startTimeSpan))
            {
                filteredData = filteredData.Where(t => t.Time >= startTimeSpan);
            }

            if (!string.IsNullOrEmpty(filter.EndTime) && TimeSpan.TryParse(filter.EndTime, out var endTimeSpan))
            {
                filteredData = filteredData.Where(t => t.Time <= endTimeSpan);
            }

            // Получаем отфильтрованные данные
            var data = filteredData.ToList();

            // Подготавливаем данные для графика
            var viewModel = new TemperatureChartViewModel
            {
                Temperatures = data.Select(d => d.Temperature).ToList(),
                Times = data.Select(d => d.Time.ToString(@"hh\:mm")).ToList(),
                FullTimes = data.Select(d => d.Time.ToString(@"hh\:mm\:ss")).ToList(),
                Filter = filter,
                AllDataCount = allData.Count,
                FilteredDataCount = data.Count
            };

            return View(viewModel);
        }

        // Метод для получения JSON с временами
        public async Task<IActionResult> GetAvailableTimes()
        {
            var allData = await _context.TemperatureData
                .OrderBy(t => t.Time)
                .ToListAsync();

            var times = allData
                .Select(t => t.Time.ToString(@"hh\:mm\:ss"))
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            return Json(times);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
