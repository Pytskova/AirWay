using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class PartRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PartName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(300)]
        public string Reason { get; set; }

        public string Status { get; set; } = "В ожидании"; // или Pending

        public DateTime RequestDate { get; set; } = DateTime.Now;

        // Навигационное свойство (по желанию)
        public Employee Employee { get; set; }
    }
}
