using System.Windows;
using System.Windows.Controls;
using AirlineApp.Models;
using AirlineApp.Services;
using AirlineApp.Views.Crew;

namespace AirlineApp.Views.Crew
{
    public partial class CrewDashboard : UserControl
    {
        private readonly int _employeeId;

        public CrewDashboard(Employee employee)
        {
            InitializeComponent();
            _employeeId = employee.Id;

            // по умолчанию показываем, скажем, Мои рейсы
            MainContent.Content = new CrewFlightsView(_employeeId);
        }

        private void Flights_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CrewFlightsView(_employeeId);

        private void Hours_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CrewHoursView(_employeeId);

        private void Reports_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CrewReportsView(_employeeId);

        private void Training_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CrewTrainingView(_employeeId);

        private void Qualification_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CrewQualificationView(_employeeId);

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на окно логина
            Window parent = Window.GetWindow(this);
            new LoginWindow().Show();
            parent?.Close();
        }
    }
}
