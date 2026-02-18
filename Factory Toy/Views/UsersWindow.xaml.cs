using System.Windows;
using Factory_Toy.Services;
using Factory_Toy.Models;

namespace Factory_Toy.Views
{
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            UsersGrid.ItemsSource = UserService.GetAll();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new UserEditWindow();
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = UsersGrid.SelectedItem as User;
            if (selected == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }

            var dlg = new UserEditWindow(selected);
            if (dlg.ShowDialog() == true)
                LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = UsersGrid.SelectedItem as User;
            if (selected == null)
            {
                MessageBox.Show("Выберите пользователя");
                return;
            }

            if (MessageBox.Show("Удалить пользователя?", "Подтверждение",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            UserService.Delete(selected.IdUser);
            LoadData();
        }
    }
}