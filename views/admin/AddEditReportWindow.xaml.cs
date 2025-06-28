using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using AirlineApp.Data;
using AirlineApp.Models;

namespace AirlineApp.Views
{
    public partial class AddEditReportWindow : Window
    {
        private readonly int? _reportId;

        public AddEditReportWindow(int? reportId = null)
        {
            InitializeComponent();
            _reportId = reportId;
            LoadLookups();
            if (_reportId.HasValue)
                LoadReport(_reportId.Value);
            else
                SparePartsGrid.ItemsSource = null;
        }

        private void LoadLookups()
        {
            using var db = new AirlineContext();
            AircraftBox.ItemsSource = db.Aircrafts.ToList();
            EmployeeBox.ItemsSource = db.Employees.ToList();
        }

        private void LoadReport(int id)
        {
            using var db = new AirlineContext();
            var rpt = db.TechnicalReports
                        .Include(r => r.SpareParts)
                        .Single(r => r.Id == id);
            AircraftBox.SelectedItem = db.Aircrafts.Find(rpt.AircraftRegistration);
            EmployeeBox.SelectedItem = db.Employees.Find(rpt.EmployeeId);
            DescriptionBox.Text = rpt.Description;
            SparePartsGrid.ItemsSource = rpt.SpareParts.ToList();
        }

        private void AddSpare_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditSparePartWindow(_reportId);
            window.Owner = this;
            if (window.ShowDialog() == true)
                LoadReport(_reportId.Value);
        }

        private void EditSpare_Click(object sender, RoutedEventArgs e)
        {
            if (SparePartsGrid.SelectedItem is SparePart sp)
            {
                var window = new AddEditSparePartWindow(_reportId, sp.Id);
                window.Owner = this;
                if (window.ShowDialog() == true)
                    LoadReport(_reportId.Value);
            }
            else
                MessageBox.Show("Выберите запчасть для изменения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteSpare_Click(object sender, RoutedEventArgs e)
        {
            if (SparePartsGrid.SelectedItem is SparePart sp)
            {
                if (MessageBox.Show("Удалить выбранную запчасть?", "Подтвердите", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using var db = new AirlineContext();
                    var toRemove = db.SpareParts.Find(sp.Id);
                    db.SpareParts.Remove(toRemove);
                    db.SaveChanges();
                    LoadReport(_reportId.Value);
                }
            }
            else
                MessageBox.Show("Выберите запчасть для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirlineContext();
            TechnicalReport rpt;
            if (_reportId.HasValue)
                rpt = db.TechnicalReports.Find(_reportId.Value);
            else
            {
                rpt = new TechnicalReport { ReportDate = DateTime.Now };
                db.TechnicalReports.Add(rpt);
            }

            rpt.AircraftRegistration = ((Aircraft)AircraftBox.SelectedItem).RegistrationNumber;
            rpt.EmployeeId = ((Employee)EmployeeBox.SelectedItem).Id;
            rpt.Description = DescriptionBox.Text.Trim();

            db.SaveChanges();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}