using Factory_Toy.Models;
using Factory_Toy.Views;
using System.Windows;

namespace Factory_Toy.Views
{
    public partial class AdminWindow : Window
    {
        private User _currentUser;

        public AdminWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            new ProductsWindow(_currentUser).ShowDialog();
        }


        private void Users_Click(object sender, RoutedEventArgs e)
        {
            var win = new UsersWindow();
            win.ShowDialog();
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();

            this.Close();
        }

    }
}