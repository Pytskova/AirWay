using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class MaintenanceSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AircraftRegistration { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        public string Description { get; set; }

        // Связь с сотрудником-техником
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
