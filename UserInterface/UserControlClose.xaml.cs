using Project_3___Press_Project;
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

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlClose.xaml
    /// </summary>
    public partial class UserControlClose : UserControl
    {
        public UserControlClose()
        {
            InitializeComponent();
        }

        private void ButtonCloseApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            if (UserSingleton.GetInstance.IsAuthenticated is true)
            {
                UserControl usc = new UserControlShop();
                this.Content = usc;
            }
            else
            {
                UserControl usc = new UserControlLogin();
                this.Content = usc;
            }
        }
    }
}
