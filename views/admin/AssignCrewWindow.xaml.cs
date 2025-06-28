using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Models;
using AirlineApp.Services;

namespace AirlineApp.Views.Admin
{
    public partial class AssignCrewWindow : Window
    {
        private readonly int _flightId;
        private readonly List<Employee> _allEmployees;

        public ObservableCollection<Employee> AvailableEmployees { get; }
        public ObservableCollection<Employee> AssignedEmployees { get; }

        public AssignCrewWindow(int flightId)
        {
            InitializeComponent();
            _flightId = flightId;

            // Загрузка всех сотрудников
            _allEmployees = EmployeeService.GetAllEmployees().ToList();

            // Изначально назначенные (если есть)
            var initiallyAssigned = CrewAssignmentService.GetAssigned(_flightId);

            AvailableEmployees = new ObservableCollection<Employee>(
                _allEmployees.Except(initiallyAssigned));

            AssignedEmployees = new ObservableCollection<Employee>(initiallyAssigned);

            // Настройка фильтра
            RoleFilter.ItemsSource = new[] { Role.Pilot, Role.CabinCrew, Role.Tech };
            RoleFilter.SelectedIndex = 0;

            // Привязка списков
            AvailableList.ItemsSource = AvailableEmployees;
            AssignedList.ItemsSource = AssignedEmployees;
        }

        private void RoleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var role = RoleFilter.SelectedItem as string;
            // Фильтруем полный список по роли и исключаем уже назначенных
            var filtered = _allEmployees
                .Where(emp => emp.Role == role && !AssignedEmployees.Contains(emp))
                .ToList();

            AvailableEmployees.Clear();
            foreach (var emp in filtered)
                AvailableEmployees.Add(emp);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var toAdd = AvailableList.SelectedItems.Cast<Employee>().ToList();
            foreach (var emp in toAdd)
            {
                AvailableEmployees.Remove(emp);
                AssignedEmployees.Add(emp);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var toRemove = AssignedList.SelectedItems.Cast<Employee>().ToList();
            foreach (var emp in toRemove)
            {
                AssignedEmployees.Remove(emp);
                // Если роль фильтра соответствует — вернуть в Available
                if ((RoleFilter.SelectedItem as string) == emp.Role)
                    AvailableEmployees.Add(emp);
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем назначение через сервис
            CrewAssignmentService.AssignCrew(_flightId, AssignedEmployees.ToList());
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
