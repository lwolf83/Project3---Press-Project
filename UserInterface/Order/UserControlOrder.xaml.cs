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
    public partial class UserControlOrder : UserControl
    {
        public UserControlOrder()
        {
            InitializeComponent();

            IEnumerable<Shop> shops = UserSingleton.GetInstance.GetShops();
            cmbShops.ItemsSource = shops;

            IEnumerable<Catalog> catalogs = UserSingleton.GetInstance.GetCatalog();
            cmbCatalog.ItemsSource = catalogs;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new PressContext())
            {
                Shop shop = (Shop) cmbShops.SelectedItem;
                Catalog catalog = (Catalog) cmbCatalog.SelectedItem;
                int quantity = Convert.ToInt32(txtQuantity.Text);

                Order order = new Order();
                order.Shop = shop;
                order.ShopId = shop.ShopId;
                order.User = UserSingleton.GetInstance.User;
                order.UserId = UserSingleton.GetInstance.User.UserId;
                context.Orders.Update(order);
                context.SaveChanges();

                OrderCatalog newOrder = new OrderCatalog();
                //newOrder.Catalog = catalog;
                newOrder.CatalogId = catalog.CatalogId;
                newOrder.Quantity = quantity;
                //newOrder.Order = order;
                newOrder.OrderId = order.OrderId;
              
                context.OrderCatalogs.Update(newOrder);
                context.SaveChanges();



            }
               
        }

    }
}