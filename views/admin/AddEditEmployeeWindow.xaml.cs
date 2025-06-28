using System;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class AddEditEmployeeWindow : Window
    {
        private readonly int? _empId;

        public AddEditEmployeeWindow(int? empId = null)
        {
            InitializeComponent();
            _empId = empId;
            if (_empId.HasValue)
                LoadEmployee(_empId.Value);
        }

        private void LoadEmployee(int id)
        {
            using var db = new AirlineContext();
            var emp = db.Employees.Find(id);
            UsernameBox.Text = emp.Username;
            PasswordBox.Password = emp.PasswordHash;
            FullNameBox.Text = emp.FullName;
            RoleBox.SelectedIndex = RoleBox.Items.IndexOf(emp.Role);
            FlightHoursBox.Text = emp.FlightHours.ToString();
            MedCertPicker.SelectedDate = emp.MedicalCertificateExpiry;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirlineContext();
            var emp = _empId.HasValue ? db.Employees.Find(_empId) : new Employee();
            if (!_empId.HasValue)
                db.Employees.Add(emp);

            emp.Username = UsernameBox.Text.Trim();
            emp.PasswordHash = PasswordBox.Password.Trim();
            emp.FullName = FullNameBox.Text.Trim();
            emp.Role = ((ComboBoxItem)RoleBox.SelectedItem).Content.ToString();
            emp.FlightHours = int.TryParse(FlightHoursBox.Text, out var fh) ? fh : 0;
            emp.MedicalCertificateExpiry = MedCertPicker.SelectedDate ?? DateTime.Now.AddYears(1);
            emp.Status = EmploymentStatus.Active;

            db.SaveChanges();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}