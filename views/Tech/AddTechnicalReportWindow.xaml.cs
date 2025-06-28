using System;
using System.Linq;
using System.Windows;
using AirlineApp.Models;
using AirlineApp.Data;

namespace AirlineApp.Views.Tech
{
    public partial class AddTechnicalReportWindow : Window
    {
        private readonly int _employeeId;

        public AddTechnicalReportWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            ReportDatePicker.SelectedDate = DateTime.Today;

            using (var db = new AirlineContext())
            {
                var aircrafts = db.Aircrafts
                    .Select(a => a.RegistrationNumber)
                    .ToList();
                AircraftBox.ItemsSource = aircrafts;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var selectedAircraft = AircraftBox.SelectedItem as string;
            string description = DescriptionBox.Text.Trim();
            DateTime? date = ReportDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(selectedAircraft) || string.IsNullOrWhiteSpace(description) || date == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            using (var db = new AirlineContext())
            {
                var report = new TechnicalReport
                {
                    EmployeeId = _employeeId,
                    AircraftRegistration = selectedAircraft,
                    ReportDate = date.Value,
                    Description = description
                };
                db.TechnicalReports.Add(report);
                db.SaveChanges();
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
