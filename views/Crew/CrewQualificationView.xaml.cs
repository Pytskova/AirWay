using System.Linq;
using System.Windows.Controls;
using AirlineApp.Data;

namespace AirlineApp.Views
{
    public partial class CrewQualificationView : UserControl
    {
        public CrewQualificationView(int crewId)
        {
            InitializeComponent();
            LoadQualifications(crewId);
        }

        private void LoadQualifications(int crewId)
        {
            using var db = new AirlineContext();
            var qualifications = db.Qualifications
                .Where(q => q.EmployeeId == crewId)
                .Select(q => new
                {
                    q.Name,
                    q.Level,
                    q.IssueDate,
                    q.ExpiryDate
                })
                .ToList();

            QualGrid.ItemsSource = qualifications;
        }
    }
}
