using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Admin
{
    public partial class AddEditFlightWindow : Window
    {
        public string WindowTitle { get; set; }
        public List<string> Registrations { get; }
        public List<KeyValuePair<FlightStatus, string>> StatusesDisplay { get; }
        public Flight EditingFlight { get; private set; }
        public FlightStatus SelectedStatus { get; set; }

        public AddEditFlightWindow(Flight flight = null)
        {
            InitializeComponent();

            // 1) Загружаем список регистраций из уже существующих рейсов
            using (var db = new AirlineContext())
            {
                Registrations = db.Flights
                                  .Select(f => f.AircraftRegistration)
                                  .Distinct()
                                  .ToList();
            }

            // 2) Статусы на русском
            StatusesDisplay = new List<KeyValuePair<FlightStatus, string>>
            {
                new(FlightStatus.Scheduled, "Запланирован"),
                new(FlightStatus.Delayed,   "Задержан"),
                new(FlightStatus.Completed, "Выполнен"),
                new(FlightStatus.Cancelled, "Отменён")
            };

            // 3) Создаём новую или копируем существующую сущность
            if (flight == null)
            {
                WindowTitle = "Добавить рейс";
                EditingFlight = new Flight
                {
                    ScheduledDeparture = DateTime.Now,
                    ScheduledArrival = DateTime.Now.AddHours(2),
                    Status = FlightStatus.Scheduled,
                    AircraftRegistration = Registrations.FirstOrDefault()
                };
            }
            else
            {
                WindowTitle = "Изменить рейс";
                EditingFlight = new Flight
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    Route = flight.Route,
                    AircraftRegistration = flight.AircraftRegistration,
                    ScheduledDeparture = flight.ScheduledDeparture,
                    ScheduledArrival = flight.ScheduledArrival,
                    ActualDeparture = flight.ActualDeparture,
                    ActualArrival = flight.ActualArrival,
                    Status = flight.Status
                };
            }

            SelectedStatus = EditingFlight.Status;
            DataContext = this;

            // Инициализируем поля DatePicker
            DpScheduledDeparture.SelectedDate = EditingFlight.ScheduledDeparture;
            DpScheduledArrival.SelectedDate = EditingFlight.ScheduledArrival;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(EditingFlight.FlightNumber) ||
                string.IsNullOrWhiteSpace(EditingFlight.Route) ||
                string.IsNullOrWhiteSpace(EditingFlight.AircraftRegistration) ||
                !DpScheduledDeparture.SelectedDate.HasValue ||
                !DpScheduledArrival.SelectedDate.HasValue)
            {
                MessageBox.Show("Заполните все поля.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Сохраняем выбранные значения
            EditingFlight.ScheduledDeparture = DpScheduledDeparture.SelectedDate.Value;
            EditingFlight.ScheduledArrival = DpScheduledArrival.SelectedDate.Value;
            EditingFlight.Status = SelectedStatus;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
