using System.Linq;
using System.Windows;
using Factory_Toy.Models;
using Factory_Toy.Services;

namespace Factory_Toy.Views
{
    public partial class ClientOrdersWindow : Window
    {
        private User _currentUser;

        public ClientOrdersWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

            LoadOrders();
        }

        private void LoadOrders()
        {
            var allOrders = OrderService.GetAll();
            var myOrders = allOrders.Where(o => o.IdUser == _currentUser.IdUser).ToList();

            OrdersList.ItemsSource = myOrders;
        }

    }
}