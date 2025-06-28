// Services/GeneralReportService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Data;
using AirlineApp.ViewModels;

namespace AirlineApp.Services
{
    public static class GeneralReportService
    {
        public static List<GeneralReportItem> GetGeneralReport()
        {
            using var db = new AirlineContext();

            // 1) Загрузка всех рейсов с экипажем и пилотскими отчётами
            var flights = db.Flights
                .Include(f => f.CrewAssignments)
                    .ThenInclude(ca => ca.Employee)
                .Include(f => f.PilotReports)
                .ToList();

            var result = new List<GeneralReportItem>();

            foreach (var f in flights)
            {
                // 2) Берём самый свежий отчёт пилота
                var pr = f.PilotReports
                          .OrderByDescending(r => r.ReportDate)
                          .FirstOrDefault();

                // 3) Берём самое свежее ТО по регистрационному номеру
                var ms = db.MaintenanceSchedules
                           .Where(m => m.AircraftRegistration == f.AircraftRegistration)
                           .OrderByDescending(m => m.MaintenanceDate)
                           .FirstOrDefault();

                result.Add(new GeneralReportItem
                {
                    FlightId = f.Id,
                    FlightNumber = f.FlightNumber,
                    Route = f.Route,
                    AircraftRegistration = f.AircraftRegistration,

                    ScheduledDeparture = f.ScheduledDeparture,
                    ScheduledArrival = f.ScheduledArrival,

                    ActualDeparture = pr?.ActualDeparture,
                    ActualArrival = pr?.ActualArrival,
                    FuelUsed = pr?.FuelUsed,
                    DelayDuration = pr?.DelayDuration,
                    DelayReason = pr?.DelayReason,
                    TechnicalIssues = pr?.TechnicalIssues,
                    WeatherNotes = pr?.WeatherNotes,
                    ReportDate = pr?.ReportDate,

                    CrewList = string.Join(", ",
                                               f.CrewAssignments
                                                .Select(ca => ca.Employee.FullName)),

                    MaintenanceDate = ms?.MaintenanceDate,
                    MaintenanceNotes = ms?.Description
                });
            }

            // 4) Сортировка по дате вылета
            return result
                   .OrderBy(r => r.ScheduledDeparture)
                   .ToList();
        }
    }
}
