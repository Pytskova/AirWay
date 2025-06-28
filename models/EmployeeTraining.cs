// Models/EmployeeTraining.cs

using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int Id { get; set; }              // Явный PK

        [Required]
        public int EventId { get; set; }
        public TrainingEvent TrainingEvent { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public bool Passed { get; set; }
    }
}
