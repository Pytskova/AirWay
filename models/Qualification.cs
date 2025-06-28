using System;

namespace AirlineApp.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        /// <summary>
        /// Тип квалификации (например, “A320 Type Rating”).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровень или категория (например, I, II или Senior/Junior).
        /// </summary>
        public string Level { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Навигация к сотруднику
        public Employee Employee { get; set; }
    }
}