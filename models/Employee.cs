using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public enum EmploymentStatus { Active, OnLeave, Terminated }

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }

        [MaxLength(100)]

        public int FlightHours { get; set; }

        public DateTime MedicalCertificateExpiry { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public EmploymentStatus Status { get; set; }

        // Navigation
        public ICollection<CrewAssignment> CrewAssignments { get; set; }
        public ICollection<TechnicalReport> TechnicalReports { get; set; }
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
        public ICollection<PilotReport> PilotReports { get; set; } = new List<PilotReport>();
        public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}
