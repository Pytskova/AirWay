using System.Windows;
using System.Windows.Controls;
using AirlineApp.Models;
using AirlineApp.Views.Admin; // Для ManageFlightsView и AssignCrewWindow

namespace AirlineApp.Views.Admin
{
    public partial class AdminDashboard : UserControl
    {
        private readonly Employee _currentEmployee;

        public AdminDashboard(Employee currentEmployee)
        {
            InitializeComponent();
            _currentEmployee = currentEmployee;
            MainContent.Content = new ManageEmployeesView();
        }

        private void BtnManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageEmployeesView();
        }

        private void BtnManageFlights_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageFlightsView();
        }

        private void BtnManageAircraft_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageAircraftView();
        }

        private void BtnManageReports_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageReportsView();
        }

        

        private void BtnSpareParts_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SparePartsAdminView();
        }

        private void BtnMaintenanceSchedule_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MaintenanceScheduleAdminView();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на окно входа
            new LoginWindow().Show();
            Window.GetWindow(this)?.Close();
        }

        private void BtnTrainingEvents_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageTrainingEventsView();
        }


    }
}
