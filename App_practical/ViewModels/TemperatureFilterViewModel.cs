using System.ComponentModel.DataAnnotations;

namespace _4_Calculator.ViewModels
{
    public class TemperatureFilterViewModel
    {
        [Display(Name = "Начальное время")]
        public string? StartTime { get; set; }

        [Display(Name = "Конечное время")]
        public string? EndTime { get; set; }

        // Списки доступных времен для выпадающих списков
        public List<string> AvailableTimes { get; set; } = new List<string>();
    }

}
