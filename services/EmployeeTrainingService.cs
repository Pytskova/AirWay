using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Services
{
    public static class EmployeeTrainingService
    {
        /// <summary>Возвращает список назначений на обучение для конкретного сотрудника.</summary>
        public static List<EmployeeTraining> GetAssignmentsForEmployee(int employeeId)
        {
            using var db = new AirlineContext();
            return db.EmployeeTrainings
                     .Include(et => et.TrainingEvent)  // подтягиваем тему и дату
                     .Where(et => et.EmployeeId == employeeId)
                     .ToList();
        }
    }
}
