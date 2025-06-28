using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class ManageEmployeesView : UserControl
    {
        public ManageEmployeesView()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using var db = new AirlineContext();
            EmployeesDataGrid.ItemsSource = db.Employees.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditEmployeeWindow();
            window.Owner = Window.GetWindow(this);
            if (window.ShowDialog() == true)
                LoadEmployees();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is Employee emp)
            {
                var window = new AddEditEmployeeWindow(emp.Id);
                window.Owner = Window.GetWindow(this);
                if (window.ShowDialog() == true)
                    LoadEmployees();
            }
            else
                MessageBox.Show("Выберите сотрудника для изменения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is Employee emp)
            {
                if (MessageBox.Show("Удалить выбранного сотрудника?", "Подтвердите", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using var db = new AirlineContext();
                    db.Employees.Remove(db.Employees.Find(emp.Id));
                    db.SaveChanges();
                    LoadEmployees();
                }
            }
            else
                MessageBox.Show("Выберите сотрудника для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
