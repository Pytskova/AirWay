using System;
using System.Windows;
using AirlineApp.Services;
using AirlineApp.Models;
using airlineApp.Services;

namespace AirlineApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Аутентификация пользователя
                var employee = AuthService.Authenticate(
                    tbUsername.Text.Trim(),
                    tbPassword.Password.Trim());

                if (employee == null)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Открываем главное окно и передаем объект сотрудника
                var mainWindow = new MainWindow(employee);
                mainWindow.Show();
                this.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
