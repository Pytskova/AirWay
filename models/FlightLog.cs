using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineApp.Models
{
    public class FlightLog
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Comments { get; set; }

        public ICollection<CrewAssignment> CrewAssignments { get; set; }

        [NotMapped]
        public double DurationHours => Math.Max(0, (ArrivalTime - DepartureTime).TotalHours);
    }
}
