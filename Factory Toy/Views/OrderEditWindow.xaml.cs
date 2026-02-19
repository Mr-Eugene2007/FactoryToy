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

            OrderDateBox.Text = order.OrderDate.ToString("yyyy-MM-dd");
            UserIdBox.Text = order.IdUser.ToString();
            TotalAmountBox.Text = order.TotalAmount.ToString();
            PrepaymentBox.Text = order.Prepayment.ToString();
            StatusBox.Text = order.Status;
            DesiredDateBox.Text = order.DesiredDate?.ToString("yyyy-MM-dd");
            ActualDateBox.Text = order.ActualDate?.ToString("yyyy-MM-dd");
            DeliveryAddressBox.Text = order.DeliveryAddress;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(OrderDateBox.Text, out DateTime orderDate))
            {
                MessageBox.Show("Дата заказа неверна");
                return;
            }

            if (!int.TryParse(UserIdBox.Text, out int userId))
            {
                MessageBox.Show("ID пользователя должно быть числом");
                return;
            }

            if (!decimal.TryParse(TotalAmountBox.Text, out decimal total))
            {
                MessageBox.Show("Сумма должна быть числом");
                return;
            }

            if (!decimal.TryParse(PrepaymentBox.Text, out decimal prepay))
            {
                MessageBox.Show("Предоплата должна быть числом");
                return;
            }

            DateTime? desired = null;
            if (DateTime.TryParse(DesiredDateBox.Text, out DateTime d))
                desired = d;

            DateTime? actual = null;
            if (DateTime.TryParse(ActualDateBox.Text, out DateTime a))
                actual = a;

            _order.OrderDate = orderDate;
            _order.IdUser = userId;
            _order.TotalAmount = total;
            _order.Prepayment = prepay;
            _order.Status = StatusBox.Text;
            _order.DesiredDate = desired;
            _order.ActualDate = actual;
            _order.DeliveryAddress = DeliveryAddressBox.Text;

            if (_isEdit)
                OrderService.Update(_order);
            else
                OrderService.Add(_order);

            DialogResult = true;
            Close();
        }
    }
}