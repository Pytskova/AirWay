using System;
using System.Linq;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Tech
{
    public partial class RequestPartWindow : Window
    {
        private readonly int _employeeId;

        public RequestPartWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadComboData();
        }

        private void LoadComboData()
        {
            using var db = new AirlineContext();

            // Подгружаем список регистрационных номеров как строки
            var regs = db.Aircrafts
                         .Select(a => a.RegistrationNumber)
                         .ToList();
            AircraftBox.ItemsSource = regs;

            // Подгружаем список названий деталей
            var parts = db.SpareParts
                          .Select(p => p.PartName)
                          .ToList();
            PartNameBox.ItemsSource = parts;
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            // Валидация выбора
            if (AircraftBox.SelectedItem is not string selectedAircraft ||
                PartNameBox.SelectedItem is not string selectedPart)
            {
                MessageBox.Show("Выберите бортовой номер и деталь.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            // Валидация количества
            if (!int.TryParse(QuantityBox.Text.Trim(), out int qty) || qty < 1)
            {
                MessageBox.Show("Введите корректное количество.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var reason = ReasonBox.Text.Trim();

            // Создаём запись заявки
            using var db = new AirlineContext();
            var req = new PartRequest
            {
                EmployeeId = _employeeId,
                PartName = selectedPart,
                Quantity = qty,
                Reason = reason,
                RequestDate = DateTime.Now,
                Status = "В ожидании"
                // Обратите внимание:
                // в вашей модели PartRequest нет поля для хранения регистрационного номера.
                // Если нужно сохранять selectedAircraft – добавьте, пожалуйста, строкевое свойство,
                // например AircraftRegistration, и включите его в модель/миграцию.
            };

            db.PartRequests.Add(req);
            db.SaveChanges();

            MessageBox.Show("Заявка успешно отправлена!",
                            "Успех",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            // Закрываем окно
            DialogResult = true;
            Close();
        }
    }
}
