using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using Project_3___Press_Project;
using System.Text.RegularExpressions;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlStock.xaml
    /// </summary>
    public partial class UserControlStock : UserControl
    {
        public IEnumerable<ShopCatalog> AllShopCatalogs { get; set; }
        public Shop ShopFilter = new Shop();

        public UserControlStock(Shop shop = null)
        {
            InitializeComponent();
            DataContext = this;
            AllShopCatalogs = ShopFilter.GetAllShopCatalogs();
            cmbShops.ItemsSource = UserSingleton.GetInstance.AllShops;

            if (shop != null)
            {
                foreach (Shop item in cmbShops.Items)
                {
                    if (item.ShopId == shop.ShopId)
                    {
                        cmbShops.SelectedItem = item;
                    }
                }
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            Shop selectedShop = (Shop)cmbShops.SelectedItem;
            if (selectedShop != null)
            {
                stockList.ItemsSource = ShopFilter.GetShopCatalogsByShop(selectedShop.ShopId);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
           
            string npaperName = ((ShopCatalog)obj).Catalog.Newspaper.Name;
            var npPrice = ((ShopCatalog)obj).Catalog.Newspaper.Price;
            var periodicity = ((ShopCatalog)obj).Catalog.Newspaper.Periodicity;
            string EAN13 = ((ShopCatalog)obj).Catalog.Newspaper.EAN13;
            var editor = ((ShopCatalog)obj).Catalog.Newspaper.Editor.Name;

            UserControlModifyNewspaper usc = new UserControlModifyNewspaper();
            usc.NewsName.Text = npaperName;
            usc.Price.Text = Convert.ToString(npPrice);
            usc.Period.Text = Convert.ToString(periodicity);
            usc.Editor.Text = editor;
            usc.eanNum.Text = EAN13;  
            this.Content = usc;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Quantity.Background = Brushes.White;
            ShopCatalog stock = (ShopCatalog)stockList.SelectedItem;
            if (stock == null)
            {
                DialogBox.OK("Select a stock of the journal whose next edition you want to order!", "No stock chosen");
            }
            else
            {
                if (Quantity.Text == String.Empty || Convert.ToInt32(Quantity.Text) > 200)
                {
                    Quantity.Background = Brushes.PaleVioletRed;
                    DialogBox.OK("You can order 0 to 200 newspapers per edition", "Provide a valid quantity!");
                }
                else
                {
                    int quantity = Convert.ToInt32(Quantity.Text);
                    OrderNextEdition(stock, quantity);
                }
            }
        }

        private void OrderNextEdition(ShopCatalog stock, int quantity)
        {
            Newspaper np = stock.Catalog.Newspaper;
            Shop shop = stock.Shop;
            bool nextEditions = np.Catalogs.Where(n => n.PublicationDate > DateTime.Today).Any();

            if (nextEditions)
            {
                var nextEdition = np.Catalogs.Where(n => n.PublicationDate > DateTime.Today)
                                 .FirstOrDefault();
                bool placed = OrderAction.CreateOrder(shop, nextEdition, quantity);
                if (placed)
                {
                    DialogBox.OK("Your order has been successfully placed", "Success");
                }
            }
            else
            {
                DialogBox.OK("According to our database, the selected newspaper has no future additions. Sorry!", "No future additions");
            }
        }
        
        private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
