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
        public ShopFilter ShopFilter = new ShopFilter();
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
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromACity(AllShops, selectedCity);
        }


        public void OnDepartmentsSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Department selectedDepartment = (Department)(sender as ComboBox).SelectedItem;
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromADepartment(AllShops, selectedDepartment);
        }

        public void OnProvincesSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Province selectedProvince = (Province)(sender as ComboBox).SelectedItem;
            ShopFilter shopFilter = new ShopFilter();
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
        }

        private void ButtonAddShopPage_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlAddShop();
            this.Content = usc;
        }
    }
}
