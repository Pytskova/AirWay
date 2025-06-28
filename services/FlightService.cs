using System.Collections.Generic;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Services
{
    public static class FlightService
    {
        public static List<Flight> GetAllFlights()
        {
            using var db = new AirlineContext();
            return db.Flights.ToList();
        }

        public static void AddFlight(Flight flight)
        {
            using var db = new AirlineContext();
            db.Flights.Add(flight);
            db.SaveChanges();
        }

        public static void UpdateFlight(Flight flight)
        {
            using var db = new AirlineContext();
            db.Flights.Update(flight);
            db.SaveChanges();
        }

        public static void DeleteFlight(int flightId)
        {
            using var db = new AirlineContext();
            var f = db.Flights.Find(flightId);
            if (f != null)
            {
                db.Flights.Remove(f);
                db.SaveChanges();
            }
        }
    }
}
