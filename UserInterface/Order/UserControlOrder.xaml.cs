using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;

namespace UserInterface
{
    public partial class UserControlOrder : UserControl
    {
        public UserControlOrder(Shop shop = null)
        {
            InitializeComponent();
            cmbShops.ItemsSource = UserSingleton.Instance.GetShops();
            cmbCatalog.ItemsSource = UserSingleton.Instance.GetCatalog();
            if(shop != null)
            {
                foreach(Shop item in cmbShops.Items)
                {
                    if(item.ShopId == shop.ShopId)
                    {
                        cmbShops.SelectedItem = item;
                    }
                }
            }
            lvOrderCatalog.ItemsSource = UserSingleton.Instance.GetOrderCatalogs();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = (Shop)cmbShops.SelectedItem;
            Catalog catalog = (Catalog)cmbCatalog.SelectedItem;
            
            if (IsValidOrderParameters(shop, catalog, txtQuantity.Text))
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                OrderAction.CreateOrder(shop, catalog, quantity);
                DisplayCurrent();
            }
        }

        private bool IsValidOrderParameters(Shop shop, Catalog catalog, string quantity)
        {
            bool shopOk = !(shop is null);
            bool catalogOk = !(catalog is null);

            bool quantityOk = !String.IsNullOrWhiteSpace(quantity);
            quantityOk = quantityOk && quantity.All(char.IsDigit);
            quantityOk = quantityOk && Convert.ToInt32(quantity) < 200;

            return shopOk && catalogOk && quantityOk;
        }

        public void BtnSeeHistoriqueCommand(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;
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
            OrderAction.PutCurrentInProduction();
            DisplayCurrent();
        }

        private void DisplayHistory()
        {
            BtnSeeHistory.Content = "See current order";
            lvOrderCatalog.ItemsSource = UserSingleton.Instance.GetOrderCatalogs("In Production");
            BtnSeeHistory.Tag = "history";
            BtnValidateOrder.Visibility = Visibility.Collapsed;
        }

        private void DisplayCurrent()
        {
            BtnSeeHistory.Content = "See order history";
            lvOrderCatalog.ItemsSource = UserSingleton.Instance.GetOrderCatalogs();
            BtnSeeHistory.Tag = "current";
            BtnValidateOrder.Visibility = Visibility.Visible;
        }

        private void lvOrderCatalog_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var context = new PressContext();

            OrderCatalog orderCatalog = (OrderCatalog) ((ListView)sender).SelectedItem;
            string tag = (string)BtnSeeHistory.Tag;
            if (tag == "current")
            {
               string msgtext = "Are you sure to delete this order ?";
               string txt = "Delete Order";
               bool resultDeleteQuestion = DialogBox.YesOrNoCancel(msgtext, txt);

               if (resultDeleteQuestion)
               {
                  context.OrderCatalogs.Remove(orderCatalog);
                  context.SaveChanges();
                  DisplayCurrent();
               }
            }
            
        }
    }
}