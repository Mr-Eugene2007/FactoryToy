using Dapper;
using Factory_Toy.Models;
using Factory_Toy.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Factory_Toy.Views
{
    public partial class ProductEditWindow : Window
    {
        private Product _product;
        private bool _isEdit;

        public ProductEditWindow()
        {
            InitializeComponent();
            _product = new Product();
            _isEdit = false;

            LoadCategories();
        }

        public ProductEditWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            _isEdit = true;

            LoadCategories();

            NameBox.Text = product.Name;
            DescriptionBox.Text = product.Description;

            CategoryCombo.SelectedValue = product.IdCategory;

            RetailPriceBox.Text = product.RetailPrice.ToString();
            CostPriceBox.Text = product.CostPrice.ToString();
            StockBox.Text = product.StockQuantity.ToString();

            StatusCombo.Text = product.Status;
        }

        private void LoadCategories()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var categories = conn.Query<Category>(
                    "SELECT idcategory, categoryname FROM productcategories"
                ).ToList();

                CategoryCombo.ItemsSource = categories;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryCombo.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            if (!decimal.TryParse(RetailPriceBox.Text, out decimal retail))
            {
                MessageBox.Show("Цена должна быть числом");
                return;
            }

            if (!decimal.TryParse(CostPriceBox.Text, out decimal cost))
            {
                MessageBox.Show("Себестоимость должна быть числом");
                return;
            }

            if (!int.TryParse(StockBox.Text, out int stock))
            {
                MessageBox.Show("Количество должно быть числом");
                return;
            }

            _product.Name = NameBox.Text;
            _product.Description = DescriptionBox.Text;
            _product.IdCategory = (int)CategoryCombo.SelectedValue;
            _product.RetailPrice = retail;
            _product.CostPrice = cost;
            _product.StockQuantity = stock;

            if (StatusCombo.SelectedItem is ComboBoxItem item)
                _product.Status = item.Content.ToString();

            if (_isEdit)
                ProductService.Update(_product);
            else
                ProductService.Add(_product);

            DialogResult = true;
            Close();
        }
    }
}