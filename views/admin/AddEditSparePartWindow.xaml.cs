using System;
using System.Windows;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class AddEditSparePartWindow : Window
    {
        private readonly int? _reportId;
        private readonly int? _partId;

        public AddEditSparePartWindow(int? reportId, int? partId = null)
        {
            InitializeComponent();
            _reportId = reportId;
            _partId = partId;
            if (_partId.HasValue)
                LoadPart(_partId.Value);
        }

        private void LoadPart(int partId)
        {
            using var db = new AirlineContext();
            var part = db.SpareParts.Find(partId);
            PartNameBox.Text = part.PartName;
            QuantityBox.Text = part.Quantity.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirlineContext();
            SparePart part;
            if (_partId.HasValue)
                part = db.SpareParts.Find(_partId.Value);
            else
            {
                part = new SparePart { Id = _reportId.Value };
                db.SpareParts.Add(part);
            }

            part.PartName = PartNameBox.Text.Trim();
            part.Quantity = int.TryParse(QuantityBox.Text, out var q) ? q : 0;

            db.SaveChanges();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}