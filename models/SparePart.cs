using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class SparePart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TechnicalReportId { get; set; }
        public TechnicalReport TechnicalReport { get; set; }

        [Required]
        [MaxLength(100)]
        public string PartName { get; set; }

        public int Quantity { get; set; }

        // --- добавляем связь с сотрудником ---
        [Required]
        public int EmployeeId { get; set; }      // Id техника, добавившего деталь

        public Employee Employee { get; set; }   // Навигационное свойство (можно не заполнять вручную)
    }
}
