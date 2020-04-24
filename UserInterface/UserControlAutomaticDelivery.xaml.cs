using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlAutomaticDelivery.xaml
    /// </summary>
    public partial class UserControlAutomaticDelivery : UserControl
    {
        public UserControlAutomaticDelivery()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            List<Shop> shops = UserSingleton.GetInstance.GetShops().ToList();
            List<AutomaticOrder> automaticOrders = AutomaticOrderLoader.GetFromShop(shops);
            lvDataBinding.ItemsSource = automaticOrders.OrderBy(x => x.Shop.Name);
        }

        private void ModifyAutomaticOrder_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AutomaticOrder automaticOrder = button.CommandParameter as AutomaticOrder;
            UserControlAddAutomaticDelivery userControlAddAutomaticDelivery = new UserControlAddAutomaticDelivery(automaticOrder);
            this.Content = userControlAddAutomaticDelivery;
        }

        private void DeleteAutomaticOrder_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AutomaticOrder currentAutomaticOrder = button.CommandParameter as AutomaticOrder;

            bool confirmDelete = DialogBox.YesOrNoCancel("Are you sur to delete this Automatic Delevery?", "Delete validation");

            if (confirmDelete)
            {
                currentAutomaticOrder.Delete();
                InitializeListView();
            }

        }
    }
}
