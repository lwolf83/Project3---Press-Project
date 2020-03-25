using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class UserControlLogout : UserControl
    {
        public UserControlLogout()
        {
            InitializeComponent();
            
            if (UserSingleton.GetInstance.IsAuthenticated is false)
            {
                UserControl usc = new UserControlLogin();
                this.Content = usc;
            }           
        }

        private void ButtonLogoutApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton.GetInstance.Disconnect();
            UserControl usc = new UserControlLogin();
            this.Content = usc;
        }

        private void ButtonLogoutApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlShop();
            this.Content = usc;
        }
    }
}
