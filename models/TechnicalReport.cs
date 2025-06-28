using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class TechnicalReport
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string AircraftRegistration { get; set; }
        public Aircraft Aircraft { get; set; }

        public DateTime ReportDate { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<SparePart> SpareParts { get; set; }
    }
}
