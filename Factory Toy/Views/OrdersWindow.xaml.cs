using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            OrdersGrid.ItemsSource = OrderService.GetAll();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderEditWindow();
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = OrdersGrid.SelectedItem as Order;
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            var dlg = new OrderEditWindow(selected);
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = OrdersGrid.SelectedItem as Order;
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            if (MessageBox.Show("Удалить заказ?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            OrderService.Delete(selected.IdOrder);
            LoadData();
        }
    }
}