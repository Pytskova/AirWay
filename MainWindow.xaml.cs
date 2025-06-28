using System.Windows;
using AirlineApp.Models;
using AirlineApp.Views.Admin;
using AirlineApp.Views.Crew;
using AirlineApp.Views.Tech;

namespace AirlineApp
{
    public partial class MainWindow : Window
    {
        private readonly Employee _currentEmployee;

        public MainWindow(Employee employee)
        {
            InitializeComponent();
            _currentEmployee = employee;
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            switch (_currentEmployee.Role)
            {
                case Role.Admin:
                    MainContent.Content = new AdminDashboard(_currentEmployee);
                    break;
                case Role.Pilot:
                    MainContent.Content = new CrewDashboard(_currentEmployee);
                    break;
                case Role.Tech:
                    MainContent.Content = new TechnicalDashboard(_currentEmployee);
                    break;
                default:
                    MessageBox.Show(
                        $"Нет поддержки роли «{_currentEmployee.Role}»",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Close();
                    break;
            }
        }
    }
}
