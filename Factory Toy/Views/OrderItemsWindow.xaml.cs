using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class OrderItemsWindow : Window
    {
        public OrderItemsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ItemsGrid.ItemsSource = OrderItemService.GetAll();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OrderItemEditWindow();
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = ItemsGrid.SelectedItem as OrderItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            var dlg = new OrderItemEditWindow(selected);
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = ItemsGrid.SelectedItem as OrderItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            if (MessageBox.Show("Удалить позицию заказа?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            OrderItemService.Delete(selected.IdOrderItem);
            LoadData();
        }
    }
}