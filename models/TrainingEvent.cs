// Models/TrainingEvent.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineApp.Models
{
    public class TrainingEvent
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Topic { get; set; }

        public DateTime EventDate { get; set; }

        [Required]


        // ← НАВИГАЦИОННОЕ СВОЙСТВО для связи многие-к-многим
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
            = new List<EmployeeTraining>();
    }
}
