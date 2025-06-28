using System;

namespace AirlineApp.ViewModels
{
    /// <summary>
    /// DTO для вывода в одном гриде и пилотских, и технических отчётов.
    /// </summary>
    public class ReportListItem
    {
        public int Id { get; set; }
        public string ReportType { get; set; }  // "Пилотский" или "Технический"
        public string AircraftRegistration { get; set; }
        public DateTime Date { get; set; }  // ReportDate или MaintenanceDate
        public string EmployeeName { get; set; }
        public string Description { get; set; }
    }
}
