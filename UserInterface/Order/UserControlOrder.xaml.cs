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
            bool shopNotOk = (shop is null);
            if (shopNotOk)
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

            if (!shopNotOk && !catalogNotOk && !quantityNotOk)
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                OrderCatalogAction.Add(shop, catalog, quantity);
                lvOrderCatalog.ItemsSource = UserSingleton.GetInstance.GetOrderCatalogs();
                DisplayCurrent();
            }
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

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}