using System.Windows;
using System.Windows.Controls;
using System.Linq;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Admin
{
    public partial class MaintenanceScheduleAdminView : UserControl
    {
        public MaintenanceScheduleAdminView()
        {
            InitializeComponent();
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            using var db = new AirlineContext();
            var data = db.MaintenanceSchedules
                .Select(r => new
                {
                    r.Id,
                    r.AircraftRegistration,
                    r.MaintenanceDate,
                    r.Description
                }).ToList();
            ScheduleGrid.ItemsSource = data;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddEditMaintenanceScheduleWindow();
            if (wnd.ShowDialog() == true)
                LoadSchedule();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleGrid.SelectedItem is null) return;
            var id = (int)ScheduleGrid.SelectedValue;
            var wnd = new AddEditMaintenanceScheduleWindow(id);
            if (wnd.ShowDialog() == true)
                LoadSchedule();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleGrid.SelectedItem is null) return;
            var row = ScheduleGrid.SelectedItem;
            int id = (int)row.GetType().GetProperty("Id").GetValue(row);
            if (MessageBox.Show("Удалить запись?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using var db = new AirlineContext();
                var obj = db.MaintenanceSchedules.FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    db.MaintenanceSchedules.Remove(obj);
                    db.SaveChanges();
                    LoadSchedule();
                }
            }
        }
    }
}
