using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Data;
using AirlineApp.Models;
using AirlineApp.Services;

namespace AirlineApp.Views.Admin
{
    public partial class ManageTrainingEventsView : UserControl
    {
        public ManageTrainingEventsView()
        {
            InitializeComponent();
            LoadTrainingEvents();
            LoadEmployees();
            LoadEventsForAssignment();
            LoadAssignments();
        }

        // ---------------- Вкладка "Курсы" ----------------

        private void LoadTrainingEvents()
        {
            var events = TrainingEventService.GetAllEvents();
            EventsGrid.ItemsSource = events;
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddEditTrainingEventWindow();
            dlg.Owner = Window.GetWindow(this);
            if (dlg.ShowDialog() == true)
                LoadTrainingEvents();
        }

        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (EventsGrid.SelectedItem is TrainingEvent selected)
            {
                var dlg = new AddEditTrainingEventWindow(selected);
                dlg.Owner = Window.GetWindow(this);
                if (dlg.ShowDialog() == true)
                    LoadTrainingEvents();
            }
            else
            {
                MessageBox.Show("Сначала выберите курс.",
                                "Внимание",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        // -------------- Вкладка "Назначения" --------------

        private void LoadEmployees()
        {
            using var db = new AirlineContext();
            EmployeeBox.ItemsSource = db.Employees
                                        .OrderBy(e => e.FullName)
                                        .ToList();
        }

        private void LoadEventsForAssignment()
        {
            using var db = new AirlineContext();
            EventBox.ItemsSource = db.TrainingEvents
                                     .OrderBy(te => te.EventDate)
                                     .ToList();
        }

        private void LoadAssignments()
        {
            using var db = new AirlineContext();
            var list = db.EmployeeTrainings
                         .Include(et => et.Employee)
                         .Include(et => et.TrainingEvent)
                         .OrderBy(et => et.Employee.FullName)
                         .ThenBy(et => et.TrainingEvent.EventDate)
                         .ToList();
            AssignmentsGrid.ItemsSource = list;
        }

        private void AssignToEvent_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeBox.SelectedItem is not Employee emp)
            {
                MessageBox.Show("Выберите сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (EventBox.SelectedItem is not TrainingEvent te)
            {
                MessageBox.Show("Выберите курс.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new AirlineContext();

            bool exists = db.EmployeeTrainings
                            .Any(et => et.EmployeeId == emp.Id && et.EventId == te.Id);
            if (exists)
            {
                MessageBox.Show("Этот сотрудник уже назначен на выбранный курс.",
                                "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            db.EmployeeTrainings.Add(new EmployeeTraining
            {
                EmployeeId = emp.Id,
                EventId = te.Id,
                Passed = false
            });
            db.SaveChanges();

            LoadAssignments();
        }
    }
}
