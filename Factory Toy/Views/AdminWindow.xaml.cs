using System.Windows;
using Factory_Toy.Views;

namespace Factory_Toy.Views
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductsWindow();
            win.ShowDialog();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            var win = new UsersWindow();
            win.ShowDialog();
        }

        private void OrderItems_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrderItemsWindow();
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