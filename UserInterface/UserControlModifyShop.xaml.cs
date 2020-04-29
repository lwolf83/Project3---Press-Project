using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlModifyShop.xaml
    /// </summary>
    public partial class UserControlModifyShop : UserControl
    {
        private readonly ShopRepository _shopRepository;
        private readonly CityRepository _cityRepository;

        public UserControlModifyShop()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Hidden;
        }

        private void ButtonFill_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = _shopRepository.FindByName(ShopName.Text);
            if (shop != null)
            {
                Autocompletor.City.Text = shop.Address.City.Name;
                StreetName.Text = shop.Address.StreetName;
                StreetNumber.Text = shop.Address.StreetNumber;
                ZipCode.Content = shop.Address.City.ZipCode;
                Department.Content = shop.Address.City.Department.Name;
            }
            else
            {
                MessageGrid.Background = Brushes.IndianRed;
                Message.Content = "Enter an existing Shop name";
                MessageGrid.Visibility = Visibility.Visible;
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilled)
            {
                string zip = Convert.ToString(ZipCode.Content);
                Shop shop = _shopRepository.FindByName(ShopName.Text);
                shop.Address.StreetName = StreetName.Text;
                shop.Address.StreetNumber = StreetNumber.Text;
                shop.Address.City = _cityRepository.FindByName(Autocompletor.City.Text);

                MessageGrid.Background = Brushes.LightGreen;
                Message.Content = "Successfully modified!";
                MessageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Empty fields detected !";
                MessageGrid.Visibility = Visibility.Visible;
            }
        }

        private bool IsFormFilled
        {
            get
            {
                return (ShopName.Text == String.Empty) ||
                       (Autocompletor.City.Text == String.Empty) ||
                       (StreetName.Text == String.Empty) ||
                       (StreetNumber.Text == String.Empty);
            }
        }
    }
}
