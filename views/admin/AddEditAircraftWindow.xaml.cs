using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class AddEditAircraftWindow : Window
    {
        private readonly bool _isNew;
        private readonly string _originalRegNumber;

        public AddEditAircraftWindow(string regNumber = null)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(regNumber))
            {
                Title = "Добавить самолёт";
                _isNew = true;
                RegBox.IsReadOnly = false;    // разрешаем ввод
            }
            else
            {
                Title = "Редактировать самолёт";
                _isNew = false;
                _originalRegNumber = regNumber;
                RegBox.Text = regNumber;
                RegBox.IsReadOnly = true;     // не даём менять ключ
                // загружаем данные
                using var db = new AirlineContext();
                var ac = db.Aircrafts.Find(regNumber);
                if (ac != null)
                {
                    ModelBox.Text = ac.Model;
                    CapacityBox.Text = ac.Capacity.ToString();
                    StatusBox.SelectedItem = ac.Status.ToString();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // валидация key
            var reg = RegBox.Text.Trim();
            if (string.IsNullOrEmpty(reg))
            {
                MessageBox.Show("Введите бортовой номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // валидация model
            var model = ModelBox.Text.Trim();
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Введите модель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // валидация capacity
            if (!int.TryParse(CapacityBox.Text.Trim(), out var cap) || cap < 0)
            {
                MessageBox.Show("Введите корректную вместимость", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // валидация status
            if (StatusBox.SelectedItem is not ComboBoxItem cbi ||
                !Enum.TryParse<AircraftStatus>((string)cbi.Content, out var status))
            {
                MessageBox.Show("Выберите статус", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AirlineContext();
            Aircraft ac;

            if (_isNew)
            {
                // новый объект — сначала задаём все свойства
                ac = new Aircraft
                {
                    RegistrationNumber = reg,
                    Model = model,
                    Capacity = cap,
                    Status = status
                };
                // теперь добавляем в контекст
                db.Aircrafts.Add(ac);
            }
            else
            {
                // редактируем существующий
                ac = db.Aircrafts.Find(_originalRegNumber);
                if (ac == null)
                {
                    MessageBox.Show("Не удалось найти самолёт для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ac.Model = model;
                ac.Capacity = cap;
                ac.Status = status;
                // EF отследит изменения автоматически
            }

            db.SaveChanges();
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
