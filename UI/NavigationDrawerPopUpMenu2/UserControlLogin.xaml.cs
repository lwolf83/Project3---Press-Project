using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NavigationDrawerPopUpMenu2
{
    public partial class UserControlLogin : UserControl
    {
        public UserControlLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            IAuthentification authentification = new Authentification();
             txtPasswordBox.Password = authentification.CryptPassword(txtPasswordBox.Password);

            if (authentification.LoginUsers(txtUserNameBox.Text, txtPasswordBox.Password))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.notConnected = false;
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Not valid Username or/and Password");
                Reset();
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            txtUserNameBox.Text = String.Empty;
            txtPasswordBox.Password = String.Empty;
        }
    }
}
