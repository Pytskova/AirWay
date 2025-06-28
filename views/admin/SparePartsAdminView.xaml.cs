using System.Windows.Controls;
using AirlineApp.Data;
using System.Linq;

namespace AirlineApp.Views.Admin
{
    public partial class SparePartsAdminView : UserControl
    {
        public SparePartsAdminView()
        {
            InitializeComponent();
            LoadParts();
            LoadRequests();
        }

        private void LoadParts()
        {
            using var db = new AirlineContext();
            var parts = db.SpareParts.Select(p => new
            {
                p.PartName,
                p.Quantity,
            }).ToList();
            PartsGrid.ItemsSource = parts;
        }

        private void LoadRequests()
        {
            using var db = new AirlineContext();
            var requests = db.PartRequests.Select(r => new
            {
                EmployeeName = r.Employee.FullName,
                r.PartName,
                r.Quantity,
                r.Reason,
                r.Status,
                r.RequestDate
            }).ToList();
            RequestsGrid.ItemsSource = requests;
        }
    }
}
