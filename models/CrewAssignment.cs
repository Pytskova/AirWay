using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public partial class CrewAssignment
    {
        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [MaxLength(50)]
        public string AssignedRole { get; set; }
    }
}