using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Services
{
    /// <summary>
    /// Сервис для чтения и сохранения назначений экипажа на рейс.
    /// </summary>
    public static class CrewAssignmentService
    {
        /// <summary>
        /// Возвращает список сотрудников, уже назначенных на заданный рейс.
        /// </summary>
        public static List<Employee> GetAssigned(int flightId)
        {
            using var db = new AirlineContext();
            return db.CrewAssignments
                     .Where(ca => ca.FlightId == flightId)
                     .Include(ca => ca.Employee)
                     .Select(ca => ca.Employee)
                     .ToList();
        }

        /// <summary>
        /// Перезаписывает состав экипажа на рейсе.
        /// </summary>
        /// <param name="flightId">ID рейса</param>
        /// <param name="employees">Список сотрудников, которых нужно назначить</param>
        public static void AssignCrew(int flightId, List<Employee> employees)
        {
            using var db = new AirlineContext();

            // 1) Удаляем старые назначения
            var existing = db.CrewAssignments
                             .Where(ca => ca.FlightId == flightId);
            db.CrewAssignments.RemoveRange(existing);
            db.SaveChanges();

            // 2) Добавляем новые, заполняя AssignedRole
            foreach (var emp in employees)
            {
                db.CrewAssignments.Add(new CrewAssignment
                {
                    FlightId = flightId,
                    EmployeeId = emp.Id,
                    AssignedRole = emp.Role   // теперь не будет NULL
                });
            }

            db.SaveChanges();
        }
    }
}
