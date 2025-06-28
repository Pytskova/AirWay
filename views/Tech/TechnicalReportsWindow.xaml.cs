using System.Windows.Controls;
using System.Windows;
using AirlineApp.Data;
using System.Linq;

namespace AirlineApp.Views.Tech
{
    public partial class TechnicalReportsWindow : UserControl
    {
        private readonly int _employeeId;

        public TechnicalReportsWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadReports();
        }

        private void LoadReports()
        {
            using var db = new AirlineContext();
            var reports = db.TechnicalReports
                .Where(r => r.EmployeeId == _employeeId)
                .Select(r => new
                {
                    r.Id,
                    r.AircraftRegistration,
                    r.ReportDate,
                    r.Description
                })
                .OrderByDescending(r => r.ReportDate)
                .ToList();
            ReportsGrid.ItemsSource = reports;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddTechnicalReportWindow(_employeeId);
            if (wnd.ShowDialog() == true)
                LoadReports();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReportsGrid.SelectedItem is not null)
            {
                dynamic selected = ReportsGrid.SelectedItem;
                int reportId = selected.Id;
                var wnd = new EditTechnicalReportWindow(reportId, _employeeId);
                if (wnd.ShowDialog() == true)
                    LoadReports();
            }
            else
                MessageBox.Show("Выберите отчёт для изменения.");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => LoadReports();
    }
}
