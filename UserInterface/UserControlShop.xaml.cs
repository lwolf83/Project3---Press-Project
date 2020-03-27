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
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetAllShops();
            CityNameFilteringSelection.ItemsSource = shopFilter.GetCitiesHavingShops();
            DepartmentNameFilteringSelection.ItemsSource = shopFilter.GetDepartmentsHavingShops();
            ProvinceNameFilteringSelection.ItemsSource = shopFilter.GetProvincesHavingShops();
        }

        public string CityFilteringSelection { get => Name; }
        public string DepartmentFilteringSelection { get => Name; }
        public string ProvinceFilteringSelection { get => Name; }


        public void OnLocationFilteringSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)(sender as ComboBox).SelectedItem;

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
            String selectedCity = (String) (sender as ComboBox).SelectedItem;
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromACity(selectedCity);
        }


        public void OnDepartmentsSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            String selectedDepartment = (String)(sender as ComboBox).SelectedItem;
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromADepartment(selectedDepartment);
        }

        public void OnProvincesSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            String selectedProvince = (String)(sender as ComboBox).SelectedItem;
            ShopFilter shopFilter = new ShopFilter();
            ShopDisplaying_ListView.ItemsSource = shopFilter.GetShopsFromAProvince(selectedProvince);
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
            Guid shopId = ((ShopCity)obj).ShopId;

            UserControl usc = new UserControlDisplayShopDetails(shopId);
            this.Content = usc;
        }
    }
}
