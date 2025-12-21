namespace _4_Calculator.ViewModels
{
    public class TemperatureChartViewModel
    {
        public List<double> Temperatures { get; set; } = new List<double>();
        public List<string> Times { get; set; } = new List<string>(); // Для отображения на графике (без секунд)
        public List<string> FullTimes { get; set; } = new List<string>(); // Полное время с секундами
        public TemperatureFilterViewModel Filter { get; set; } = new TemperatureFilterViewModel();
        public int AllDataCount { get; set; }
        public int FilteredDataCount { get; set; }
    }
}
