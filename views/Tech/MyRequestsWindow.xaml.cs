using System.Windows.Controls;
using System.Windows;
using AirlineApp.Data;
using System.Linq;

namespace AirlineApp.Views.Tech
{
    public partial class MyRequestsWindow : UserControl
    {
        private readonly int _employeeId;

        public MyRequestsWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadRequests();
        }

        private void LoadRequests()
        {
            using var db = new AirlineContext();
            var reqs = db.PartRequests
                .Where(r => r.EmployeeId == _employeeId)
                .OrderByDescending(r => r.RequestDate)
                .Select(r => new
                {
                    r.Id,
                    r.PartName,
                    r.Quantity,
                    r.Status,
                    r.Reason,
                    r.RequestDate
                }).ToList();
            RequestsGrid.ItemsSource = reqs;
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is not null)
            {
                dynamic selected = RequestsGrid.SelectedItem;
                int reqId = selected.Id;
                using (var db = new AirlineContext())
                {
                    var req = db.PartRequests.FirstOrDefault(r => r.Id == reqId && r.EmployeeId == _employeeId && r.Status == "В ожидании");
                    if (req != null)
                    {
                        db.PartRequests.Remove(req);
                        db.SaveChanges();
                        LoadRequests();
                    }
                    else
                        MessageBox.Show("Заявку уже нельзя удалить (выполнена или отклонена)!");
                }
            }
            else
                MessageBox.Show("Выберите заявку для удаления.");
        }
    }
}
