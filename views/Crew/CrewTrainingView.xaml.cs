using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views.Crew
{
    public partial class CrewTrainingView : UserControl
    {
        private readonly int _employeeId;

        public CrewTrainingView(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            using var db = new AirlineContext();

            // Загружаем все назначения для данного сотрудника, вместе с самим событием обучения
            var list = db.EmployeeTrainings
                         .Include(et => et.TrainingEvent)
                         .Where(et => et.EmployeeId == _employeeId)
                         .OrderBy(et => et.TrainingEvent.EventDate)
                         .ToList();

            TrainingGrid.ItemsSource = list;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAssignments();
        }
    }
}
