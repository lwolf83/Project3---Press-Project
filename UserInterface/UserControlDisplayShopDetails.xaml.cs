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
            ShopFilter shopFilter = new ShopFilter();
            GridForm.DataContext = shopFilter.GetShopDetail(shopId);
        }
    }
}
