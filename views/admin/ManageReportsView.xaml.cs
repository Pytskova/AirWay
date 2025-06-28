using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Services;
using AirlineApp.ViewModels;

namespace AirlineApp.Views.Admin
{
    public partial class ManageReportsView : UserControl
    {
        public ObservableCollection<ReportListItem> Reports { get; }

        public ManageReportsView()
        {
            InitializeComponent();
            Reports = new ObservableCollection<ReportListItem>();
            DataContext = this;
            Refresh_Click(null, null);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Reports.Clear();
            var pilots = PilotReportService.GetPilotReportItems();
            var techs = TechnicalReportService.GetTechnicalReportItems();
            foreach (var item in pilots.Concat(techs).OrderBy(r => r.Date))
                Reports.Add(item);
        }
    }
}
