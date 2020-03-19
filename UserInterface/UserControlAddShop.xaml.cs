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
    /// Interaction logic for UserControlAddShop.xaml
    /// </summary>
    public partial class UserControlAddShop : UserControl
    {
        public UserControlAddShop()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            txtUserNameBox.Text = String.Empty;
            txtPasswordBox.Password = String.Empty;
        }
    }
}
