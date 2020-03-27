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
<<<<<<< HEAD

            lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();          

=======
>>>>>>> master
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new PressContext())
            {
                Shop shop = (Shop) cmbShops.SelectedItem;
                Catalog catalog = (Catalog) cmbCatalog.SelectedItem;
                int quantity = Convert.ToInt32(txtQuantity.Text);
<<<<<<< HEAD
                OrderCatalogAction.Add(shop, catalog, quantity);
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
                DisplayCurrent();
            }
=======
>>>>>>> master

                Order order = new Order();
                order.Shop = shop;
                order.ShopId = shop.ShopId;
                order.User = UserSingleton.GetInstance.User;
                order.UserId = UserSingleton.GetInstance.User.UserId;
                context.Orders.Update(order);
                context.SaveChanges();

<<<<<<< HEAD
        }

        public void BtnDeleteOrderCommand(object sender, RoutedEventArgs e)
        { 
            
            using(var context = new PressContext())
            {
                var orderCatalog = ((ListViewItem)sender).Content;
                string tag = (string) BtnSeeHistory.Tag;
                if (tag == "current")
                { 
                    string msgtext = "Are you sure to delete this order ?";
                    string txt = "Delete Order";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            context.Remove(orderCatalog);
                            context.SaveChanges();
                            DisplayCurrent();
                            break;
                        case MessageBoxResult.No:
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }

            }
          
        }

        public void BtnSeeHistoriqueCommand(object sender, RoutedEventArgs e)
        {
            string tag = (string) ((Button)sender).Tag;
            if (tag == "current")
            {
                DisplayHistory();
            }
            else
            {
                DisplayCurrent();
            }
        }

        public void BtnValidateOrderCommand(object sender, RoutedEventArgs e)
        {
            OrderCatalogAction.PutOrderCurrentInProduction();
            DisplayCurrent();

        }



        private void DisplayHistory()
        {
            BtnSeeHistory.Content = "See current order";
            lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs("In Production");
            BtnSeeHistory.Tag = "history";
            BtnValidateOrder.Visibility = Visibility.Collapsed;
        }

        private void DisplayCurrent()
        {
            BtnSeeHistory.Content = "See order history";
            lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
            BtnSeeHistory.Tag = "current";
            BtnValidateOrder.Visibility = Visibility.Visible;
        }
=======
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

>>>>>>> master
    }
}