using Factory_Toy.Models;
using System.Windows;

namespace Factory_Toy.Views
{
    public partial class ManagerWindow : Window
    {
        private User _currentUser;

        public ManagerWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            new ProductsWindow(_currentUser).ShowDialog();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrdersWindow();
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