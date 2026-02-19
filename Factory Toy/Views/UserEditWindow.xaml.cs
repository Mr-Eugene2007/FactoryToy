using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Dapper;
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

            LoadRoles();
        }

        public UserEditWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _isEdit = true;

            LoadRoles();

            LoginBox.Text = user.Login;
            PasswordBox.Text = user.Password;
            NameBox.Text = user.FullName;
            EmailBox.Text = user.Email;
            PhoneBox.Text = user.Phone;

            RoleCombo.SelectedValue = user.IdRole;
        }

        private void LoadRoles()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var roles = conn.Query<Role>(
                    "SELECT idrole, rolename FROM roles"
                ).ToList();

                RoleCombo.ItemsSource = roles;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                MessageBox.Show("Логин обязателен");
                return;
            }

            if (RoleCombo.SelectedValue == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }

            _user.Login = LoginBox.Text;
            _user.Password = PasswordBox.Text;
            _user.FullName = NameBox.Text;
            _user.Email = EmailBox.Text;
            _user.Phone = PhoneBox.Text; // телефон сохраняем

            _user.IdRole = (int)RoleCombo.SelectedValue;

            if (_isEdit)
                UserService.Update(_user);
            else
                UserService.Add(_user);

            DialogResult = true;
            Close();
        }

        // ===== МАСКА ТЕЛЕФОНА =====

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        private void PhoneBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string digits = new string(PhoneBox.Text.Where(char.IsDigit).ToArray());

            if (digits.StartsWith("7"))
                digits = digits.Substring(1);

            if (digits.Length > 10)
                digits = digits.Substring(0, 10);

            string formatted = "+7";

            if (digits.Length > 0)
                formatted += " (" + digits.Substring(0, Math.Min(3, digits.Length));

            if (digits.Length >= 3)
                formatted += ") " + digits.Substring(3, Math.Min(3, digits.Length - 3));

            if (digits.Length >= 6)
                formatted += "-" + digits.Substring(6, Math.Min(2, digits.Length - 6));

            if (digits.Length >= 8)
                formatted += "-" + digits.Substring(8, Math.Min(2, digits.Length - 8));

            PhoneBox.Text = formatted;
            PhoneBox.SelectionStart = PhoneBox.Text.Length;
        }
    }
}