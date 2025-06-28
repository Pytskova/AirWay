using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class PilotReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required]
        public DateTime ActualDeparture { get; set; }

        [Required]
        public DateTime ActualArrival { get; set; }

        [Required]
        public double FuelUsed { get; set; }

        public int? DelayDuration { get; set; }
        public string DelayReason { get; set; }
        public string TechnicalIssues { get; set; }
        public string WeatherNotes { get; set; }
        public string Remarks { get; set; }
    }
}
