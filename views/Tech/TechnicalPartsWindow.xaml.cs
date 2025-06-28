using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirlineApp.Data;

namespace AirlineApp.Views.Tech
{
    public partial class TechnicalPartsWindow : UserControl
    {
        private readonly int _employeeId;

        // Передаём сюда текущий ID техника, чтобы прокидывать его в окно заявки
        public TechnicalPartsWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadParts();
        }

        private void LoadParts()
        {
            using var db = new AirlineContext();
            var parts = db.SpareParts
                          .Select(p => new
                          {
                              p.PartName,
                              p.Quantity
                          })
                          .ToList();
            PartsGrid.ItemsSource = parts;
        }

        private void RequestPart_Click(object sender, RoutedEventArgs e)
        {
            // Создаём и показываем модальное окно заявки
            var dialog = new RequestPartWindow(_employeeId)
            {
                Owner = Window.GetWindow(this),  // ставим владельцем текущее окно
            };
            dialog.ShowDialog();

            // После закрытия окна заявки можно, если нужно, обновить список:
            // LoadParts();
        }
    }
}
