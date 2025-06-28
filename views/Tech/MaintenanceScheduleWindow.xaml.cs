using System.Windows.Controls;
using AirlineApp.Data;
using System.Linq;

namespace AirlineApp.Views.Tech
{
    public partial class MaintenanceScheduleWindow : UserControl
    {
        private readonly int _employeeId;

        public MaintenanceScheduleWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            using var db = new AirlineContext();
            var schedule = db.MaintenanceSchedules
                .Where(r => r.EmployeeId == _employeeId)
                .Select(r => new
                {
                    r.Id,
                    r.AircraftRegistration,
                    r.MaintenanceDate,
                    r.Description
                }).ToList();
            ScheduleGrid.ItemsSource = schedule;
        }
    }
}
