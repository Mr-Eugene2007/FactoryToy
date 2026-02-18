using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ProductsGrid.ItemsSource = ProductService.GetAll();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ProductEditWindow();
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductsGrid.SelectedItem as Product;
            if (selected == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            var dlg = new ProductEditWindow(selected);
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductsGrid.SelectedItem as Product;
            if (selected == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            if (MessageBox.Show("Удалить товар?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            ProductService.Delete(selected.IdProduct);
            LoadData();
        }
    }
}