using System;
using System.Windows;
using Factory_Toy.Models;
using Factory_Toy.Services;

namespace Factory_Toy.Views
{
    public partial class OrderEditWindow : Window
    {
        private Order _order;
        private bool _isEdit;

        public OrderEditWindow()
        {
            InitializeComponent();
            _order = new Order();
            _isEdit = false;
        }

        public OrderEditWindow(Order order)
        {
            InitializeComponent();
            _order = order;
            _isEdit = true;

            OrderDateBox.Text = order.OrderDate.ToString("yyyy-MM-dd HH:mm");
            UserBox.Text = order.IdUser.ToString();
            TotalBox.Text = order.TotalAmount.ToString();
            PrepaymentBox.Text = order.Prepayment.ToString();
            StatusBox.Text = order.Status;
            DesiredBox.Text = order.DesiredDate?.ToString("yyyy-MM-dd");
            ActualBox.Text = order.ActualDate?.ToString("yyyy-MM-dd");
            AddressBox.Text = order.DeliveryAddress;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(OrderDateBox.Text, out DateTime orderDate))
            {
                MessageBox.Show("Неверная дата заказа");
                return;
            }

            if (!int.TryParse(UserBox.Text, out int userId))
            {
                MessageBox.Show("ID пользователя должно быть числом");
                return;
            }

            if (!decimal.TryParse(TotalBox.Text, out decimal total))
            {
                MessageBox.Show("Сумма должна быть числом");
                return;
            }

            if (!decimal.TryParse(PrepaymentBox.Text, out decimal prepay))
            {
                MessageBox.Show("Предоплата должна быть числом");
                return;
            }

            _order.OrderDate = orderDate;
            _order.IdUser = userId;
            _order.TotalAmount = total;
            _order.Prepayment = prepay;
            _order.Status = StatusBox.Text;
            if (DateTime.TryParse(DesiredBox.Text, out DateTime desired))
                _order.DesiredDate = desired;
            else
                _order.DesiredDate = null;

            if (DateTime.TryParse(ActualBox.Text, out DateTime actual))
                _order.ActualDate = actual;
            else
                _order.ActualDate = null;
            _order.DeliveryAddress = AddressBox.Text;

            if (_isEdit)
                OrderService.Update(_order);
            else
                OrderService.Add(_order);

            DialogResult = true;
            Close();
        }
    }
}