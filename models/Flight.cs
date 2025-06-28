using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public enum FlightStatus
    {
        Scheduled,
        Delayed,
        Completed,
        Cancelled
    }

    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string FlightNumber { get; set; }

        [Required, MaxLength(200)]
        public string Route { get; set; }

        [Required, MaxLength(20)]
        public string AircraftRegistration { get; set; }

        public Aircraft Aircraft { get; set; }

        [Required]
        public DateTime ScheduledDeparture { get; set; }

        [Required]
        public DateTime ScheduledArrival { get; set; }

        public DateTime? ActualDeparture { get; set; }


        public DateTime? ActualArrival { get; set; }

        public FlightStatus Status { get; set; }

        // Navigation
        public ICollection<CrewAssignment> CrewAssignments { get; set; } = new List<CrewAssignment>();
        public ICollection<FlightLog> FlightLogs { get; set; } = new List<FlightLog>();
        public ICollection<PilotReport> PilotReports { get; set; } = new List<PilotReport>();
    }
}
