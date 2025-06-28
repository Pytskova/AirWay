using System;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Admin
{
    public partial class AddEditTrainingEventWindow : Window
    {
        private readonly bool _isNew;
        public TrainingEvent EditingEvent { get; private set; }

        public AddEditTrainingEventWindow(TrainingEvent ev = null)
        {
            InitializeComponent();

            if (ev == null)
            {
                Title = "Добавить обучение";
                _isNew = true;
                EditingEvent = new TrainingEvent
                {
                    EventDate = DateTime.Today
                };
            }
            else
            {
                Title = "Редактировать обучение";
                _isNew = false;
                EditingEvent = new TrainingEvent
                {
                    Id = ev.Id,
                    Topic = ev.Topic,
                    EventDate = ev.EventDate
                };
            }

            // Предзаполняем UI
            TbTopic.Text = EditingEvent.Topic;
            DpEventDate.SelectedDate = EditingEvent.EventDate;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(TbTopic.Text))
            {
                MessageBox.Show("Введите тему.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!DpEventDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Записываем изменения в модель
            EditingEvent.Topic = TbTopic.Text.Trim();
            EditingEvent.EventDate = DpEventDate.SelectedDate.Value;

            // Сохраняем в БД
            using var db = new AirlineContext();
            if (_isNew)
                db.TrainingEvents.Add(EditingEvent);
            else
                db.TrainingEvents.Update(EditingEvent);

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
