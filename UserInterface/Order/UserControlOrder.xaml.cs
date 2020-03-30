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

            cmbShops.ItemsSource = UserSingleton.GetInstance.GetShops();
            cmbCatalog.ItemsSource = UserSingleton.GetInstance.GetCatalog();
            lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = (Shop)cmbShops.SelectedItem;
            Catalog catalog = (Catalog)cmbCatalog.SelectedItem;
            
            if (IsValidOrderParameters(shop, catalog, txtQuantity.Text))
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                Order order = OrderFactory.Create(UserSingleton.GetInstance.User, shop, DateTime.Now);
                OrderCatalog orderCatalog;
                if (OrderCatalog.Exist(order, catalog))
                {
                    string msgtext = "An order already exist. Do you want to replace it ?";
                    string txt = "Delete Order";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(msgtext, txt, button);
                    if (result == MessageBoxResult.Yes)
                    {
                        orderCatalog = OrderCatalogFactory.Load(order, catalog);
                        orderCatalog.Quantity = quantity;
                        orderCatalog.Save();
                        orderCatalog.ActivateOrder("In progress");
                    }
                }
                else
                {
                    orderCatalog = OrderCatalogFactory.Create(order, catalog);
                    orderCatalog.Quantity = quantity;
                    orderCatalog.Save();
                    orderCatalog.ActivateOrder("In progress");
                }
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

        public void BtnDeleteOrderCommand(object sender, RoutedEventArgs e)
        {

            using (var context = new PressContext())
            {
                var orderCatalog = ((ListViewItem)sender).Content;
                string tag = (string)BtnSeeHistory.Tag;
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
            OrderCatalogFactory.PutOrderCurrentInProduction();
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


    }
}