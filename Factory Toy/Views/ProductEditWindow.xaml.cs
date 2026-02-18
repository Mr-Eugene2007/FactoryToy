using System;
using System.Windows;
using Factory_Toy.Models;
using Factory_Toy.Services;

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
        }

        public ProductEditWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            _isEdit = true;

            NameBox.Text = product.Name;
            DescriptionBox.Text = product.Description;
            CategoryBox.Text = product.IdCategory.ToString();
            RetailPriceBox.Text = product.RetailPrice.ToString();
            CostPriceBox.Text = product.CostPrice.ToString();
            StockBox.Text = product.StockQuantity.ToString();
            StatusBox.Text = product.Status;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Название обязательно");
                return;
            }

            if (!int.TryParse(CategoryBox.Text, out int category))
            {
                MessageBox.Show("Категория должна быть числом");
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
            _product.IdCategory = category;
            _product.RetailPrice = retail;
            _product.CostPrice = cost;
            _product.StockQuantity = stock;
            _product.Status = StatusBox.Text;

            if (_isEdit)
                ProductService.Update(_product);
            else
                ProductService.Add(_product);

            DialogResult = true;
            Close();
        }
    }
}