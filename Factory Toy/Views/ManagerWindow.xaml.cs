using System.Windows;

namespace Factory_Toy.Views
{
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrdersWindow();
            win.ShowDialog();
        }

        private void OrderItems_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrderItemsWindow();
            win.ShowDialog();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductsWindow();
            win.ShowDialog();
        }
    }
}