using System;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;

namespace airlineApp.Services
{
    public static class AuthService
    {
        /// <summary>
        /// Аутентифицирует пользователя и возвращает объект Employee.
        /// Если логин или пароль неверны — выбрасывает UnauthorizedAccessException.
        /// </summary>
        public static Employee Authenticate(string username, string password)
        {
            using var db = new AirlineContext();
            var user = db.Employees
                         .SingleOrDefault(e => e.Username == username);
            if (user == null || user.PasswordHash != password)
                throw new UnauthorizedAccessException("Неверный логин или пароль");

            return user;
        }
    }
}
