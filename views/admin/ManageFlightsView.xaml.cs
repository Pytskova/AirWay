using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AirlineApp.Models;
using AirlineApp.Services;
using AirlineApp.Views.Admin;

namespace AirlineApp.Views.Admin
{
    public partial class ManageFlightsView : UserControl
    {
        public ObservableCollection<Flight> Flights { get; }
        public ICommand AssignCrewCommand { get; }

        public ManageFlightsView()
        {
            InitializeComponent();

            // 1) Загрузка рейсов
            Flights = new ObservableCollection<Flight>(FlightService.GetAllFlights());

            // 2) Команда для «Назначить экипаж»
            AssignCrewCommand = new RelayCommand<int>(flightId =>
            {
                var win = new AssignCrewWindow(flightId)
                {
                    Owner = Window.GetWindow(this)
                };
                if (win.ShowDialog() == true)
                {
                    MessageBox.Show("Экипаж успешно назначен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });

            DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddEditFlightWindow();
            win.Owner = Window.GetWindow(this);
            if (win.ShowDialog() == true)
            {
                FlightService.AddFlight(win.EditingFlight);
                Flights.Add(win.EditingFlight);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (FlightsGrid.SelectedItem is Flight selected)
            {
                var win = new AddEditFlightWindow(selected);
                win.Owner = Window.GetWindow(this);
                if (win.ShowDialog() == true)
                {
                    FlightService.UpdateFlight(win.EditingFlight);
                    // Обновляем в коллекции
                    var idx = Flights.IndexOf(selected);
                    Flights[idx] = win.EditingFlight;
                }
            }
            else
            {
                MessageBox.Show("Выберите рейс для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FlightsGrid.SelectedItem is Flight selected)
            {
                var result = MessageBox.Show(
                    $"Удалить рейс {selected.FlightNumber}?",
                    "Подтвердите удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    FlightService.DeleteFlight(selected.Id);
                    Flights.Remove(selected);
                }
            }
            else
            {
                MessageBox.Show("Выберите рейс для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
