using System.Windows;

namespace Factory_Toy.Views
{
    public partial class ProductionManagerWindow : Window
    {
        public ProductionManagerWindow()
        {
            InitializeComponent();
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
