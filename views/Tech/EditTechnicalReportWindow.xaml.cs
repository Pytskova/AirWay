using System;
using System.Windows;
using AirlineApp.Models;
using AirlineApp.Data;
using System.Linq;

namespace AirlineApp.Views.Tech
{
    public partial class EditTechnicalReportWindow : Window
    {
        private readonly int _reportId;
        private readonly int _employeeId;
        public EditTechnicalReportWindow(int reportId, int employeeId)
        {
            InitializeComponent();
            _reportId = reportId;
            _employeeId = employeeId;
            LoadReport();
        }

        private void LoadReport()
        {
            using var db = new AirlineContext();
            var report = db.TechnicalReports.FirstOrDefault(r => r.Id == _reportId && r.EmployeeId == _employeeId);
            if (report != null)
            {
                AircraftBox.Text = report.AircraftRegistration;
                ReportDatePicker.SelectedDate = report.ReportDate;
                DescriptionBox.Text = report.Description;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string aircraft = AircraftBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();
            DateTime? date = ReportDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(aircraft) || string.IsNullOrWhiteSpace(description) || date == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            using (var db = new AirlineContext())
            {
                var report = db.TechnicalReports.FirstOrDefault(r => r.Id == _reportId && r.EmployeeId == _employeeId);
                if (report != null)
                {
                    report.AircraftRegistration = aircraft;
                    report.ReportDate = date.Value;
                    report.Description = description;
                    db.SaveChanges();
                }
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
