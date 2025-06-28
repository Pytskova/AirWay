using System;
using System.Linq;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Crew
{
    public partial class CrewHoursView : UserControl
    {
        private readonly int _employeeId;

        public CrewHoursView(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadFlightHours();
        }

        private void LoadFlightHours()
        {
            using var db = new AirlineContext();

            // Берём все отчёты пилота для данного сотрудника
            var reports = db.PilotReports
                            .Where(r => r.EmployeeId == _employeeId)
                            .Select(r => new
                            {
                                r.ReportDate,
                                FlightNumber = r.Flight.FlightNumber,
                                r.ActualDeparture,
                                r.ActualArrival,
                                // считаем продолжительность
                                DurationHours = Math.Max(0, (r.ActualArrival - r.ActualDeparture).TotalHours)
                            })
                            .ToList();

            // Привязываем к гриду
            LogsGrid.ItemsSource = reports;

            // Считаем общий налёт
            double total = reports.Sum(r => r.DurationHours);
            SummaryText.Text = $"Общий налёт: {total:F2} ч";
        }
    }
}
