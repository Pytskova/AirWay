using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Models;

namespace AirlineApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            using var context = new AirlineContext();

            context.Database.Migrate();

            if (!context.Employees.Any(e => e.Username == "admin"))
            {
                var admin = new Employee
                {
                    Username = "admin",
                    PasswordHash = "admin",
                    FullName = "Администратор",
                    Role = "Admin",
                    Status = EmploymentStatus.Active,

                    FlightHours = 0,
                    MedicalCertificateExpiry = DateTime.Now.AddYears(1)
                };

                context.Employees.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
