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

            IEnumerable<Shop> userShops = UserSingleton.GetInstance.GetShops();
            cmbShops.ItemsSource = userShops;

            IEnumerable<Catalog> catalogs = UserSingleton.GetInstance.GetCatalog();
            cmbCatalog.ItemsSource = catalogs;

            lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = (Shop)cmbShops.SelectedItem;
            bool shopNotOk = (shop is null);
            if(shopNotOk)
            {
                lblShop.Background = new SolidColorBrush(Colors.Red);
                lblShop.Foreground = new SolidColorBrush(Colors.White);
                lblShop.Content = lblShop.Content + " (Required)";
            }


            Catalog catalog = (Catalog)cmbCatalog.SelectedItem;
            bool catalogNotOk = (catalog is null);
            if (catalogNotOk)
            {
                lblCatalog.Background = new SolidColorBrush(Colors.Red);
            }

            bool quantityNotOk = String.IsNullOrWhiteSpace(txtQuantity.Text);
            if (quantityNotOk)
            {
                lblQuantity.Background = new SolidColorBrush(Colors.Red);
            }

            if(!shopNotOk && !catalogNotOk && !quantityNotOk)
            { 
                int quantity = Convert.ToInt32(txtQuantity.Text);
                OrderCatalogAction.Add(shop, catalog, quantity);
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
            }


        }

        public void BtnDeleteOrderCommand(object sender, RoutedEventArgs e)
        { 
            Guid orderCatalogIdToDelete = (Guid) ((Button)sender).Tag;
            using(var context = new PressContext())
            {
                OrderCatalog orderCatalog = context.OrderCatalogs.Where(o => o.OrderCatalogId == orderCatalogIdToDelete).FirstOrDefault();
                context.Remove(orderCatalog);
                context.SaveChanges();
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
            }
          
        }

        public void BtnSeeHistoriqueCommand(object sender, RoutedEventArgs e)
        {
            string tag = (string) ((Button)sender).Tag;
            if (tag == "current")
            {
                BtnSeeHistory.Content = "See current order";
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs("In Production");
                BtnSeeHistory.Tag = "history";
            }
            else
            {
                BtnSeeHistory.Content = "See order history";
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
                BtnSeeHistory.Tag = "current";
            }
        }

        public void BtnValidateOrderCommand(object sender, RoutedEventArgs e)
        {
            using(var context = new PressContext())
            {
                IEnumerable<OrderCatalog> orderValidated = UserSingleton.GetInstance.GetOrderCatalogs();
                foreach(OrderCatalog orderCatalog in orderValidated)
                {
                    orderCatalog.Order.State = "In Production";
                }
                context.UpdateRange(orderValidated);
                context.SaveChanges();
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
            }

        }

        public void CmdInitShops(object sender, RoutedEventArgs e)
        {
           
        
        }
    }
}