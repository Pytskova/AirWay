//---------------------------------------
// File: Models/CrewAssignment.Partial.cs
//---------------------------------------
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineApp.Models
{
    public partial class CrewAssignment
    {
        /// <summary>
        /// Флаг для UI: выбран ли сотрудник на этот рейс.
        /// Не отображается в БД.
        /// </summary>
        [NotMapped]
        public bool IsAssigned { get; set; }
    }
}
