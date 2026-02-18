using Factory_Toy.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Factory_Toy
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _passwordVisible = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginBox.Text;
                string password = PasswordBox.Password; // или PasswordBox.Password

                var user = AuthService.Login(login, password);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }

                Window next;

                switch (user.IdRole)
                {
                    case 1: // Администратор
                        next = new AdminWindow();
                        break;

                    case 2: // Менеджер по клиентам
                        next = new ManagerWindow();
                        break;

                    case 3: // Производственный менеджер
                        next = new ProductionManagerWindow();
                        break;

                    case 4: // Клиент
                        next = new ClientWindow();
                        break;

                    default:
                        MessageBox.Show("Неизвестная роль пользователя");
                        return;
                }

                next.Show();
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void ShowPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            _passwordVisible = !_passwordVisible;

            if (_passwordVisible)
            {
                PasswordText.Text = PasswordBox.Password;
                PasswordText.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
                ShowPasswordBtn.Content = "🙈";
            }
            else
            {
                PasswordBox.Password = PasswordText.Text;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordText.Visibility = Visibility.Collapsed;
                ShowPasswordBtn.Content = "👁";
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!_passwordVisible)
                return;

            PasswordText.Text = PasswordBox.Password;
        }

        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_passwordVisible)
                PasswordBox.Password = PasswordText.Text;
        }

    }
}



