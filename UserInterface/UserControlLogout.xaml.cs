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
                UserControlSetter.SetGridMain(this, "ItemLogin");
            }           
        }

        private void ButtonLogoutApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton.GetInstance.Disconnect();
            UserControlSetter.SetGridMain(this, "ItemLogin");
        }

        private void ButtonLogoutApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(this, "ItemShop");
        }
    }
}
