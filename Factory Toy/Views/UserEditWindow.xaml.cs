using System;
using System.Windows;
using Factory_Toy.Models;
using Factory_Toy.Services;

namespace Factory_Toy.Views
{
    public partial class UserEditWindow : Window
    {
        private User _user;
        private bool _isEdit;

        public UserEditWindow()
        {
            InitializeComponent();
            _user = new User();
            _isEdit = false;
        }

        public UserEditWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _isEdit = true;

            LoginBox.Text = user.Login;
            PasswordBox.Text = user.Password;
            FullNameBox.Text = user.FullName;
            EmailBox.Text = user.Email;
            PhoneBox.Text = user.Phone;
            RoleBox.Text = user.IdRole.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                MessageBox.Show("Логин обязателен");
                return;
            }

            if (!int.TryParse(RoleBox.Text, out int role))
            {
                MessageBox.Show("ID роли должно быть числом");
                return;
            }

            _user.Login = LoginBox.Text;
            _user.Password = PasswordBox.Text;
            _user.FullName = FullNameBox.Text;
            _user.Email = EmailBox.Text;
            _user.Phone = PhoneBox.Text;
            _user.IdRole = role;

            if (_isEdit)
                UserService.Update(_user);
            else
                UserService.Add(_user);

            DialogResult = true;
            Close();
        }
    }
}