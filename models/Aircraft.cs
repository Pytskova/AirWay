using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public enum AircraftStatus { Active, UnderMaintenance, Retired }

    public class Aircraft
    {
        [Key, MaxLength(20)]
        public string RegistrationNumber { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int Capacity { get; set; }

        public AircraftStatus Status { get; set; }

        // Navigation
        public ICollection<Flight> Flights { get; set; }
        public ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public ICollection<TechnicalReport> TechnicalReports { get; set; }
    }
}