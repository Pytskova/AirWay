using System;
using System.Linq;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Admin
{
    public partial class AddEditMaintenanceScheduleWindow : Window
    {
        private readonly int? _scheduleId;

        public AddEditMaintenanceScheduleWindow(int? scheduleId = null)
        {
            InitializeComponent();
            _scheduleId = scheduleId;

            // 1) Подгружаем все бортовые номера из таблицы Flights
            using (var db = new AirlineContext())
            {
                var regs = db.Flights
                             .Select(f => f.AircraftRegistration)
                             .Distinct()
                             .ToList();
                CbAircraft.ItemsSource = regs;
            }

            // 2) Подгружаем всех техников
            using (var db = new AirlineContext())
            {
                var techs = db.Employees
                              .Where(e => e.Role == Role.Tech)
                              .ToList();
                CbTechnician.ItemsSource = techs;
            }

            // 3) Если мы редактируем — заполняем поля
            if (_scheduleId.HasValue)
                LoadExisting(_scheduleId.Value);
            else
                DpDate.SelectedDate = DateTime.Now;
        }

        private void LoadExisting(int id)
        {
            using var db = new AirlineContext();
            var sched = db.MaintenanceSchedules.Find(id);
            if (sched == null) return;

            CbAircraft.SelectedItem = sched.AircraftRegistration;
            CbTechnician.SelectedValue = sched.EmployeeId;
            DpDate.SelectedDate = sched.MaintenanceDate;
            TbDescription.Text = sched.Description;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все поля заполнены
            if (CbAircraft.SelectedItem == null ||
                CbTechnician.SelectedValue == null ||
                !DpDate.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(TbDescription.Text))
            {
                MessageBox.Show("Заполните все поля.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AirlineContext();
            MaintenanceSchedule sched;

            if (_scheduleId.HasValue)
            {
                sched = db.MaintenanceSchedules.Find(_scheduleId.Value);
                if (sched == null) return;
            }
            else
            {
                sched = new MaintenanceSchedule();
                db.MaintenanceSchedules.Add(sched);
            }

            // Заполняем поля перед сохранением
            sched.AircraftRegistration = (string)CbAircraft.SelectedItem;
            sched.EmployeeId = (int)CbTechnician.SelectedValue;
            sched.MaintenanceDate = DpDate.SelectedDate.Value;
            sched.Description = TbDescription.Text.Trim();

            try
            {
                db.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении:\n{ex.InnerException?.Message ?? ex.Message}",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
