using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Services
{
    public static class TrainingEventService
    {
        /// <summary>
        /// Возвращает все учебные мероприятия (для администратора).
        /// </summary>
        public static List<TrainingEvent> GetAllEvents()
        {
            using var db = new AirlineContext();
            return db.TrainingEvents
                     .OrderBy(e => e.EventDate)
                     .ToList();
        }

        public static void AddEvent(TrainingEvent ev)
        {
            using var db = new AirlineContext();
            db.TrainingEvents.Add(ev);
            db.SaveChanges();
        }

        public static void UpdateEvent(TrainingEvent ev)
        {
            using var db = new AirlineContext();
            db.TrainingEvents.Update(ev);
            db.SaveChanges();
        }

        public static void DeleteEvent(int id)
        {
            using var db = new AirlineContext();
            var ev = db.TrainingEvents.Find(id);
            if (ev != null)
            {
                db.TrainingEvents.Remove(ev);
                db.SaveChanges();
            }
        }
    }
}
