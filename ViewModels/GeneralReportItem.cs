// ViewModels/GeneralReportItem.cs
using System;

namespace AirlineApp.ViewModels
{
    public class GeneralReportItem
    {
        // Информация о рейсе
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string Route { get; set; }
        public string AircraftRegistration { get; set; }

        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }

        // Из PilotReports (самый свежий отчёт для рейса)
        public DateTime? ActualDeparture { get; set; }
        public DateTime? ActualArrival { get; set; }
        public double? FuelUsed { get; set; }    // float в БД → double?
        public int? DelayDuration { get; set; }    // DelayDuration
        public string DelayReason { get; set; }
        public string TechnicalIssues { get; set; }
        public string WeatherNotes { get; set; }
        public DateTime? ReportDate { get; set; }    // ReportDate

        // Состав экипажа
        public string CrewList { get; set; }

        // Из MaintenanceSchedules (самый последний по дате для ВС)
        public DateTime? MaintenanceDate { get; set; }
        public string MaintenanceNotes { get; set; }
    }
}
