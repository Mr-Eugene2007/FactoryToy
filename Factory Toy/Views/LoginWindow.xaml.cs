using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = UserService.Auth(LoginBox.Text, PasswordBox.Password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            switch (user.IdRole)
            {
                case 1:
                    new AdminWindow(user).ShowDialog();
                    break;

                case 2:
                    new ManagerWindow(user).ShowDialog();
                    break;

                case 3:
                    new ProductionManagerWindow(user).ShowDialog();
                    break;

                case 4:
                    new ClientWindow(user).ShowDialog();
                    break;

                default:
                    MessageBox.Show("Неизвестная роль пользователя");
                    return;
            }

            Close();
        }
    }
}