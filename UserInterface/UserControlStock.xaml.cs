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
using System.Linq;
using Project_3___Press_Project;


namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlStock.xaml
    /// </summary>
    public partial class UserControlStock : UserControl
    {
        public IEnumerable<Shop> AllShops { get; set; }
        public IEnumerable<ShopCatalog> AllShopCatalogs { get; set; }
        public ShopFilter ShopFilter = new ShopFilter();

        public UserControlStock()
        {
            InitializeComponent();
            AllShopCatalogs = ShopFilter.GetAllShopCatalogs();
            AllShops = UserSingleton.GetInstance.AllShops;
            cmbShops.ItemsSource = AllShops;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            Shop selectedShop = (Shop)cmbShops.SelectedItem;
            stockList.ItemsSource = AllShopCatalogs.Where(sc => sc.Shop.ShopId == selectedShop.ShopId);
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
            Guid newspaperId = ((ShopCatalog)obj).Catalog.Newspaper.NewspaperId;
            string EAN13 = ((ShopCatalog)obj).Catalog.Newspaper.EAN13;
            string editorName = ((ShopCatalog)obj).Catalog.Newspaper.Editor.Name;

            UserControlModifyNewspaper usc = new UserControlModifyNewspaper();
            usc.EAN13 = EAN13;

            this.Content = usc;
        }
    }
}
