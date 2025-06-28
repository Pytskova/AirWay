using System.Windows;
using System.Windows.Controls;
using AirlineApp.Models;

namespace AirlineApp.Views.Tech
{
    public partial class TechnicalDashboard : UserControl
    {
        private readonly int _employeeId;

        // Принимаем объект Employee, но используем только Id
        public TechnicalDashboard(Employee employee)
        {
            InitializeComponent();
            _employeeId = employee.Id;
            MainContent.Content = new TechnicalReportsWindow(_employeeId);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new TechnicalReportsWindow(_employeeId);

        private void BtnSchedule_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new MaintenanceScheduleWindow(_employeeId);

        private void BtnParts_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new TechnicalPartsWindow(_employeeId);

        private void BtnRequests_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new MyRequestsWindow(_employeeId);

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}
