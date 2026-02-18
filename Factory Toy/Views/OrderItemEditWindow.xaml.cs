using System;
using System.Windows;
using Factory_Toy.Models;
using Factory_Toy.Services;

namespace Factory_Toy.Views
{
    public partial class OrderItemEditWindow : Window
    {
        private OrderItem _item;
        private bool _isEdit;

        public OrderItemEditWindow()
        {
            InitializeComponent();
            _item = new OrderItem();
            _isEdit = false;
        }

        public OrderItemEditWindow(OrderItem item)
        {
            InitializeComponent();
            _item = item;
            _isEdit = true;

            OrderBox.Text = item.IdOrder.ToString();
            ProductBox.Text = item.IdProduct.ToString();
            QuantityBox.Text = item.Quantity.ToString();
            CustomCheck.IsChecked = item.IsCustom;
            DescriptionBox.Text = item.CustomDescription;
            SketchBox.Text = item.CustomSketch;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(OrderBox.Text, out int order))
            {
                MessageBox.Show("ID заказа должно быть числом");
                return;
            }

            if (!int.TryParse(ProductBox.Text, out int product))
            {
                MessageBox.Show("ID товара должно быть числом");
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int qty))
            {
                MessageBox.Show("Количество должно быть числом");
                return;
            }

            _item.IdOrder = order;
            _item.IdProduct = product;
            _item.Quantity = qty;
            _item.IsCustom = CustomCheck.IsChecked == true;
            _item.CustomDescription = DescriptionBox.Text;
            _item.CustomSketch = SketchBox.Text;

            if (_isEdit)
                OrderItemService.Update(_item);
            else
                OrderItemService.Add(_item);

            DialogResult = true;
            Close();
        }
    }
}