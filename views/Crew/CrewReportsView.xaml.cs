using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;
using AirlineApp.Views.Crew;

namespace AirlineApp.Views.Crew
{
    public partial class CrewReportsView : UserControl
    {
        private readonly int _employeeId;

        public CrewReportsView(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadReports();
        }

        private void LoadReports()
        {
            using var db = new AirlineContext();
            var reports = db.PilotReports
                            .Where(r => r.EmployeeId == _employeeId)
                            .OrderByDescending(r => r.ReportDate)
                            .Select(r => new
                            {
                                r.Id,
                                ReportDate = r.ReportDate.ToString("dd.MM.yyyy"),
                                FlightNumber = r.Flight.FlightNumber,
                                Departure = r.ActualDeparture.ToString("HH:mm"),
                                Arrival = r.ActualArrival.ToString("HH:mm"),
                                FuelUsed = r.FuelUsed,
                                Status = r.Flight.Status.ToString()
                            })
                            .ToList();
            ReportsGrid.ItemsSource = reports;
        }

        private void AddReport_Click(object sender, RoutedEventArgs e)
        {
            // Передаём только employeeId, выбор рейса внутри окна
            var dlg = new AddPilotReportWindow(_employeeId)
            {
                Owner = Window.GetWindow(this)
            };

            if (dlg.ShowDialog() == true)
                LoadReports();
        }

        private void EditReport_Click(object sender, RoutedEventArgs e)
        {
            if (ReportsGrid.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите отчёт для редактирования.",
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            // Получаем Id из анонимного объекта
            var prop = ReportsGrid.SelectedItem.GetType().GetProperty("Id");
            if (prop == null) return;
            int reportId = (int)prop.GetValue(ReportsGrid.SelectedItem)!;

            var dlg = new EditPilotReportWindow(reportId)
            {
                Owner = Window.GetWindow(this)
            };
            if (dlg.ShowDialog() == true)
                LoadReports();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadReports();
        }
    }
}
