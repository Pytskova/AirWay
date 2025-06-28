using System.Linq;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class CrewFlightsView : UserControl
    {
        public CrewFlightsView(int crewId)
        {
            InitializeComponent();
            LoadFlights(crewId);
        }

        private void LoadFlights(int crewId)
        {
            using var db = new AirlineContext();

            var flights = db.Flights
                .Where(f => f.CrewAssignments.Any(ca => ca.EmployeeId == crewId))
                .Select(f => new
                {
                    f.FlightNumber,
                    f.Route,
                    f.AircraftRegistration,
                    f.ScheduledDeparture,
                    f.ScheduledArrival,
                    Status = f.Status.ToString()
                })
                .ToList();

            FlightsGrid.ItemsSource = flights;
        }
    }
}
