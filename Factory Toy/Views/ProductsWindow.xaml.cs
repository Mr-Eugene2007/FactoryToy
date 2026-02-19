using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class ProductsWindow : Window
    {
        private User _currentUser;

        // Конструктор для XAML-дизайнера и на случай ошибочного вызова
        public ProductsWindow()
        {
            InitializeComponent();

            // Если пользователь не передан — скрываем кнопки полностью
            AddButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;

            LoadData();
        }

        // Основной конструктор
        public ProductsWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            ApplyRoleRestrictions();
            LoadData();
        }

        private void ApplyRoleRestrictions()
        {
            // Клиент (ID = 4) не может редактировать товары
            if (_currentUser != null && _currentUser.IdRole == 4)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
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