using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Services
{
    /// <summary>
    /// Сервис для работы с сотрудниками.
    /// </summary>
    public static class EmployeeService
    {
        /// <summary>
        /// Возвращает всех сотрудников (включая навигационное свойство User, если нужно).
        /// </summary>
        public static List<Employee> GetAllEmployees()
        {
            using var db = new AirlineContext();
            // Подтягиваем информацию о пользователе, если требуется:
            return db.Employees
                     .ToList();
        }
    }
}
