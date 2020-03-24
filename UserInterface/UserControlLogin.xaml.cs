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
using Project_3___Press_Project;

namespace UserInterface
{
    public partial class UserControlLogin : UserControl
    {
        public UserControlLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton.GetInstance.Init(txtUserNameBox.Text, txtPasswordBox.Password);
            if (UserSingleton.GetInstance.IsAuthenticated)
            {
                UserControl usc = new UserControlShop();
                this.Content = usc;
            }
            else
            {
                txtMessageBox.Visibility = Visibility.Visible;
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
