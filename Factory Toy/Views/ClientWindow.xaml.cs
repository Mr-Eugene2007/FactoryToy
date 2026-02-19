using System.Windows;

namespace Factory_Toy.Views
{
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductsWindow();
            win.ShowDialog();
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