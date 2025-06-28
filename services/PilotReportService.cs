using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;
using AirlineApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Services
{
    public static class PilotReportService
    {
        public static List<ReportListItem> GetPilotReportItems()
        {
            using var db = new AirlineContext();
            return db.PilotReports
                     .Include(r => r.Flight)
                     .Include(r => r.Employee)
                     .Select(r => new ReportListItem
                     {
                         Id = r.Id,
                         ReportType = "Пилотский",
                         AircraftRegistration = r.Flight.AircraftRegistration,
                         Date = r.ReportDate,
                         EmployeeName = r.Employee.FullName,
                         Description = r.Remarks
                     })
                     .ToList();
        }
    }
}
