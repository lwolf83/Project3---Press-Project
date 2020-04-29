using System;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlDisplayShopDetails.xaml
    /// </summary>
    public partial class UserControlDisplayShopDetails : UserControl
    {
        private readonly ShopRepository _shopRepository;

        public UserControlDisplayShopDetails(Guid shopId)
        {
            _shopRepository = new ShopRepository();
            InitializeComponent();
            GridForm.DataContext = _shopRepository.GetShopDetail(shopId);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlShop();
            this.Content = usc;
        }
    }
}
