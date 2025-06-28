using System;
using System.Linq;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Crew
{
    public partial class AddPilotReportWindow : Window
    {
        private readonly int _employeeId;

        public AddPilotReportWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadFlights();
        }

        private void LoadFlights()
        {
            using var db = new AirlineContext();
            FlightBox.ItemsSource = db.Flights
                                      .OrderBy(f => f.FlightNumber)
                                      .ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // 1) Рейс
            if (FlightBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите рейс.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int flightId = (int)FlightBox.SelectedValue;

            // 2) Дата/время вылета
            if (!DpDepartureDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Укажите дату вылета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!TimeSpan.TryParse(TbDepartureTime.Text, out var depTs))
            {
                MessageBox.Show("Введите время вылета в формате ЧЧ:ММ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime actualDep = DpDepartureDate.SelectedDate.Value.Date + depTs;

            // 3) Дата/время прилёта
            if (!DpArrivalDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Укажите дату прилёта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!TimeSpan.TryParse(TbArrivalTime.Text, out var arrTs))
            {
                MessageBox.Show("Введите время прилёта в формате ЧЧ:ММ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime actualArr = DpArrivalDate.SelectedDate.Value.Date + arrTs;

            if (actualArr < actualDep)
            {
                MessageBox.Show("Время прилёта не может быть раньше времени вылета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 4) Топливо
            if (!double.TryParse(FuelBox.Text.Trim(), out var fuel) || fuel < 0)
            {
                MessageBox.Show("Введите корректное количество топлива.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 5) Задержка
            int? delay = null;
            if (int.TryParse(DelayBox.Text.Trim(), out var d) && d > 0)
                delay = d;

            using var db = new AirlineContext();

            // 6) Сохраняем PilotReport
            var report = new PilotReport
            {
                FlightId = flightId,
                EmployeeId = _employeeId,
                ReportDate = DateTime.Now,
                ActualDeparture = actualDep,
                ActualArrival = actualArr,
                FuelUsed = fuel,
                DelayDuration = delay,
                DelayReason = ReasonBox.Text.Trim(),
                TechnicalIssues = TechBox.Text.Trim(),
                WeatherNotes = WeatherBox.Text.Trim(),
                Remarks = RemarksBox.Text.Trim()
            };
            db.PilotReports.Add(report);
            db.SaveChanges();

            // 7) Сохраняем FlightLog
            var log = new FlightLog
            {
                FlightId = flightId,
                DepartureTime = actualDep,
                ArrivalTime = actualArr,
                Comments = RemarksBox.Text.Trim()
            };
            db.FlightLogs.Add(log);

            // 8) Обновляем статус рейса
            var flight = db.Flights.Find(flightId);
            if (flight != null)
                flight.Status = FlightStatus.Completed;

            db.SaveChanges();

            MessageBox.Show("Отчёт и лог рейса успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
