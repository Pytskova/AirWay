using System;
using System.Linq;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Crew
{
    public partial class EditPilotReportWindow : Window
    {
        private readonly int _reportId;

        public EditPilotReportWindow(int reportId)
        {
            InitializeComponent();
            _reportId = reportId;
            LoadReport();
        }

        private void LoadReport()
        {
            using var db = new AirlineContext();
            var r = db.PilotReports.Find(_reportId);
            if (r == null) return;

            ReportDatePicker.SelectedDate = r.ReportDate;
            DepartureDatePicker.SelectedDate = r.ActualDeparture.Date;
            DepartureTimeBox.Text = r.ActualDeparture.ToString("HH:mm");
            ArrivalDatePicker.SelectedDate = r.ActualArrival.Date;
            ArrivalTimeBox.Text = r.ActualArrival.ToString("HH:mm");
            FuelBox.Text = r.FuelUsed.ToString();
            DelayBox.Text = r.DelayDuration?.ToString() ?? "";
            DelayReasonBox.Text = r.DelayReason;
            TechIssuesBox.Text = r.TechnicalIssues;
            WeatherNotesBox.Text = r.WeatherNotes;
            RemarksBox.Text = r.Remarks;
        }

        private void UpdateReport_Click(object sender, RoutedEventArgs e)
        {
            if (ReportDatePicker.SelectedDate == null ||
                DepartureDatePicker.SelectedDate == null ||
                ArrivalDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Укажите все даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(DepartureTimeBox.Text, out var depTs) ||
                !TimeSpan.TryParse(ArrivalTimeBox.Text, out var arrTs) ||
                !double.TryParse(FuelBox.Text, out var fuelUsed))
            {
                MessageBox.Show("Проверьте формат времени и топлива.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var depDt = DepartureDatePicker.SelectedDate.Value + depTs;
            var arrDt = ArrivalDatePicker.SelectedDate.Value + arrTs;
            if (arrDt <= depDt)
            {
                MessageBox.Show("Время прилёта должно быть позже вылета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AirlineContext();
            var r = db.PilotReports.Find(_reportId);
            if (r == null) return;

            r.ReportDate = ReportDatePicker.SelectedDate.Value;
            r.ActualDeparture = depDt;
            r.ActualArrival = arrDt;
            r.FuelUsed = fuelUsed;
            r.DelayDuration = int.TryParse(DelayBox.Text, out var dd) ? dd : (int?)null;
            r.DelayReason = DelayReasonBox.Text;
            r.TechnicalIssues = TechIssuesBox.Text;
            r.WeatherNotes = WeatherNotesBox.Text;
            r.Remarks = RemarksBox.Text;

            db.SaveChanges();
            DialogResult = true;
            Close();
        }
    }
}
