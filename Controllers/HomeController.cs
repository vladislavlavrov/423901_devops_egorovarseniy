using _4_Calculator.Data;
using _4_Calculator.Models;
using _4_Calculator.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace _4_Calculator.Controllers
{
    public class HomeController : Controller
    {
        private CalculatorContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly KafkaProducerService<Null, string> _producer;
        public HomeController(CalculatorContext context, ILogger<HomeController> logger, KafkaProducerService<Null, string> producer)
        { 
            _context = context;
            _logger = logger;
            _producer = producer;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Calculator()
        {
            var data = _context.DataInputVariants
                .OrderByDescending(x => x.ID_DataInputVariant)
                .ToList();

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculator(double number1, double number2, Operation operation)
        {
            var dataInputVariant = new DataInputVariant
            {
                Operand_1 = number1,
                Operand_2 = number2,
                Type_operation = operation,
            };

            await SendDataToKafka(dataInputVariant);
            return RedirectToAction(nameof(Calculator));

            ///return RedirectToAction(nameof(Calculator));
        }
        public IActionResult Callback([FromBody] DataInputVariant inputData)
        {
            SaveDataAndResult(inputData);
            return Ok();
        }
        private DataInputVariant SaveDataAndResult(DataInputVariant inputData)
        {
            _context.DataInputVariants.Add(inputData);
            _context.SaveChanges();
            return inputData;
        }
        private async Task SendDataToKafka(DataInputVariant dataInputVariant)
        {
            var json = JsonSerializer.Serialize(dataInputVariant);
            await _producer.ProduceAsync("4_Calculator", new Message<Null, string>
            { Value = json });
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
