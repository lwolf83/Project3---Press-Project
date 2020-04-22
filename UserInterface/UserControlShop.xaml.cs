using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project_3___Press_Project;


namespace UserInterface
{
    public partial class UserControlShop : UserControl
    {
        public UserControlShop()
        {
            InitializeComponent();
            AllShops = UserSingleton.GetInstance.AllShops;
            ShopDisplaying_ListView.ItemsSource = AllShops;
            CityNameFilteringSelection.ItemsSource = ShopFilter.GetCitiesHavingShops(AllShops);
            DepartmentNameFilteringSelection.ItemsSource = ShopFilter.GetDepartmentsHavingShops(AllShops);
            ProvinceNameFilteringSelection.ItemsSource = ShopFilter.GetProvincesHavingShops(AllShops);
        }

        public string CityFilteringSelection { get => Name; }
        public string DepartmentFilteringSelection { get => Name; }
        public string ProvinceFilteringSelection { get => Name; }
        public Shop ShopFilter = new Shop();
        public MainWindow mainWindow { get => new MainWindow(); }
        public IEnumerable<Shop> AllShops { get; set; }
        

        public void OnLocationFilteringSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem) ShopFilteringEntity.SelectedItem;

            if (selectedItem.Name == "CitiesSelection")
            {
                CityNameFilteringSelection.Visibility = Visibility.Visible;
                ProvinceNameFilteringSelection.Visibility = Visibility.Collapsed;
                DepartmentNameFilteringSelection.Visibility = Visibility.Collapsed;
            }
            else if (selectedItem.Name == "DepartmentsSelection")
            {
                CityNameFilteringSelection.Visibility = Visibility.Collapsed;
                ProvinceNameFilteringSelection.Visibility = Visibility.Collapsed;
                DepartmentNameFilteringSelection.Visibility = Visibility.Visible;
            }
            else if (selectedItem.Name == "ProvincesSelection")
            {
                CityNameFilteringSelection.Visibility = Visibility.Collapsed;
                ProvinceNameFilteringSelection.Visibility = Visibility.Visible;
                DepartmentNameFilteringSelection.Visibility = Visibility.Collapsed;
            }
            else
            {
                CityNameFilteringSelection.Visibility = Visibility.Collapsed;
                ProvinceNameFilteringSelection.Visibility = Visibility.Collapsed;
                DepartmentNameFilteringSelection.Visibility = Visibility.Collapsed;
                throw new Exception();
            }
        }

        public void OnCitiesSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            City selectedCity = (City) (sender as ComboBox).SelectedItem;
            Shop shopFilter = new Shop();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromACity(AllShops, selectedCity);
        }

        public void OnDepartmentsSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Department selectedDepartment = (Department)(sender as ComboBox).SelectedItem;
            Shop shopFilter = new Shop();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromADepartment(AllShops, selectedDepartment);
        }

        public void OnProvincesSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Province selectedProvince = (Province)(sender as ComboBox).SelectedItem;
            Shop shopFilter = new Shop();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromAProvince(AllShops, selectedProvince);
        }

        private void ReinitializeShopList_Btn(object sender, RoutedEventArgs e)
        {
            UserSingleton.GetInstance.AllShops = ShopFilter.GetAllShops();
            AllShops = UserSingleton.GetInstance.AllShops;
            ShopDisplaying_ListView.ItemsSource = AllShops;
            CityNameFilteringSelection.Visibility = Visibility.Collapsed;
            ProvinceNameFilteringSelection.Visibility = Visibility.Collapsed;
            DepartmentNameFilteringSelection.Visibility = Visibility.Collapsed;
            UserControl usc = new UserControlShop();
            this.Content = usc;
        }

        private void ButtonAddShopPage_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlAddShop();
            this.Content = usc;
        }
        private void ViewShopDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
            Guid shopId = ((Shop)obj).ShopId;

            UserControl usc = new UserControlDisplayShopDetails(shopId);
            this.Content = usc;
        }

        private void ButtonSeeStocks_Click(object sender, RoutedEventArgs e)
        {
            Shop selectedShop = (Shop)ShopDisplaying_ListView.SelectedItem;
            UserControl usc;
            if(selectedShop != null)
            {
                usc = new UserControlStock(selectedShop);
            }
            else
            {
                usc = new UserControlStock();
            }
            this.Content = usc;
        }

        private void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            Shop selectedShop = (Shop) ShopDisplaying_ListView.SelectedItem;
            UserControl usc;
            if (selectedShop != null)
            {
                usc = new UserControlOrder(selectedShop);
            }
            else
            {
                usc = new UserControlOrder();
            }
            this.Content = usc;
        }
    }
}
