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
    /// Interaction logic for UserControlLogoutClose.xaml
    /// </summary>
    public partial class UserControlLogoutClose : UserControl
    {
        public UserControlLogoutClose(string text = null)
        {
            InitializeComponent();

            if (text is "Logout")
            {
                if (MainWindow.notConnected)
                {
                    UserControl usc = new UserControlLogin();
                    this.Content = usc;                    
                }
                else
                {
                    LogoutMessageBox.Visibility = Visibility.Visible;
                }
            }
            if (text is "Close")
            {
                CloseMessageBox.Visibility = Visibility.Visible;             
            }
        }

        private void ButtonLogoutApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.notConnected = true;
            LogoutMessageBox.Visibility = Visibility.Collapsed;
        }

        private void ButtonLogoutApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            LogoutMessageBox.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            CloseMessageBox.Visibility = Visibility.Collapsed;
        }
    }
}
