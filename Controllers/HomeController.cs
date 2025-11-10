using System.Diagnostics;
using _4_Calculator.Data;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class HomeController : Controller
    {
        private CalculatorContext _context;
        public HomeController(CalculatorContext context)
        { 
            _context = context;
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Calculator()
        {
            return View(new CalculatorModel()); 
        }
        [HttpPost]
        public IActionResult Calculator(double Number1, double Number2, string Operation)
        {
            var model = new CalculatorModel
            {
                Number1 = Number1,
                Number2 = Number2,
                Operation = Operation
            };

            _logger.LogInformation($"Received: {Number1} {Operation} {Number2}");

            try
            {
                model.Result = Operation switch
                {
                    "+" => Number1 + Number2,
                    "-" => Number1 - Number2,
                    "*" => Number1 * Number2,
                    "/" when Number2 != 0 => Number1 / Number2,
                    "/" when Number2 == 0 => throw new DivideByZeroException(),
                    _ => throw new ArgumentException("Неверная операция")
                };

                _logger.LogInformation($"Result: {model.Result}");
            }
            catch (DivideByZeroException)
            {
                model.ErrorMessage = "Ошибка: деление на ноль!";
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Ошибка: {ex.Message}";
            }

            DataImputVariant dataImputVariant = new DataImputVariant();
            dataImputVariant.Operand_1 = Number1.ToString();
            dataImputVariant.Operand_2 = Number2.ToString();
            dataImputVariant.Type_operation = Operation.ToString();
            
            _context.DataImputVariants.Add( dataImputVariant );
            _context.SaveChanges();

            return View(model);
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
