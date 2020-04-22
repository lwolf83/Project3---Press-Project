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
    /// <summary>
    /// Interaction logic for UserControlDisplayShopDetails.xaml
    /// </summary>
    public partial class UserControlDisplayShopDetails : UserControl
    {
        public UserControlDisplayShopDetails(Guid shopId)
        {
            InitializeComponent();
            Shop shopFilter = new Shop();
            GridForm.DataContext = shopFilter.GetShopDetail(shopId);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlShop();
            this.Content = usc;
        }
    }
}
