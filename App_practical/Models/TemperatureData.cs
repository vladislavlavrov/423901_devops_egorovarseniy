using System;
using System.ComponentModel.DataAnnotations;

namespace _4_Calculator.Models
{
    public class TemperatureData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public DateTime FullDateTime { get; set; }
    }
}
