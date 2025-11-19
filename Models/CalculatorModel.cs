namespace _4_Calculator.Models
{
    public class CalculatorModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public Operation Operation { get; set; }
        public double Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
