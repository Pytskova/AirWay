using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class ManageAircraftView : UserControl
    {
        public ManageAircraftView()
        {
            InitializeComponent();
            LoadAircraft();
        }

        private void LoadAircraft()
        {
            using var db = new AirlineContext();
            AircraftDataGrid.ItemsSource = db.Aircrafts.ToList();
        }

        private void AddPlane_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditAircraftWindow();
            window.Owner = Window.GetWindow(this);
            if (window.ShowDialog() == true)
                LoadAircraft();
        }

        private void EditPlane_Click(object sender, RoutedEventArgs e)
        {
            if (AircraftDataGrid.SelectedItem is Aircraft ac)
            {
                var window = new AddEditAircraftWindow(ac.RegistrationNumber);
                window.Owner = Window.GetWindow(this);
                if (window.ShowDialog() == true)
                    LoadAircraft();
            }
            else
                MessageBox.Show("Выберите самолёт для изменения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeletePlane_Click(object sender, RoutedEventArgs e)
        {
            if (AircraftDataGrid.SelectedItem is Aircraft ac)
            {
                if (MessageBox.Show("Удалить выбранный самолёт?", "Подтвердите", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using var db = new AirlineContext();
                    var toRemove = db.Aircrafts.Find(ac.RegistrationNumber);
                    db.Aircrafts.Remove(toRemove);
                    db.SaveChanges();
                    LoadAircraft();
                }
            }
            else
                MessageBox.Show("Выберите самолёт для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}