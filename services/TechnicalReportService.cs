using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;
using AirlineApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Services
{
    public static class TechnicalReportService
    {
        public static List<ReportListItem> GetTechnicalReportItems()
        {
            using var db = new AirlineContext();
            return db.MaintenanceSchedules
                     .Include(m => m.Employee)
                     .Select(m => new ReportListItem
                     {
                         Id = m.Id,
                         ReportType = "Технический",
                         AircraftRegistration = m.AircraftRegistration,
                         Date = m.MaintenanceDate,
                         EmployeeName = m.Employee.FullName,
                         Description = m.Description
                     })
                     .ToList();
        }
    }
}
