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
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            var user = AuthService.Login(login, password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            Window next;

            switch (user.IdRole)
            {
                case 1:
                    next = new AdminWindow();
                    break;

                case 2:
                    next = new ManagerWindow();
                    break;

                case 3:
                    next = new ProductionManagerWindow();
                    break;

                case 4:
                    next = new ClientWindow();
                    break;

                default:
                    MessageBox.Show("Неизвестная роль пользователя");
                    return;
            }

            next.Show();
            this.Close();
        }
    }
}