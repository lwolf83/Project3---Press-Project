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
using System.ComponentModel;
using System.Linq;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlModifyShop.xaml
    /// </summary>
    public partial class UserControlModifyShop : UserControl
    {
        public IEnumerable<Shop> AllShops { get; set; }
        public Shop SelectedShop { get; set; }
        public UserControlModifyShop()
        {
            InitializeComponent();
            DataContext = this;
            AllShops = UserSingleton.GetInstance.AllShops;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Hidden;
        }

        private void ButtonFill_Click(object sender, RoutedEventArgs e)
        {
            string shopName = ShopName.Text;
            bool shopExists = AllShops.Where(s => s.Name == shopName).Any();
            if (shopExists)
            {
                SelectedShop = AllShops.Where(s => s.Name == shopName).FirstOrDefault();
                Autocompletor.City.Text = SelectedShop.Adress.City.Name;
                StrName.Text = SelectedShop.Adress.StreetName;
                StrNum.Text = SelectedShop.Adress.StreetNumber;
                Zip.Content = SelectedShop.Adress.City.ZipCode;
                Dept.Content = SelectedShop.Adress.City.Department.DepartmentName;
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
            bool allFieldsFilled = !(ShopName.Text == String.Empty) && !(Autocompletor.City.Text == String.Empty)
                                    && !(StrName.Text == String.Empty) && !(StrNum.Text == String.Empty);
            if (allFieldsFilled)
            {
                string zip = Convert.ToString(Zip.Content);
                SelectedShop.ModifyShop(ShopName.Text, StrNum.Text, StrName.Text, zip, Autocompletor.City.Text);
                Reset();
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

        private void Reset()
        {
            ShopName.Text = String.Empty;
            Autocompletor.City.Text = String.Empty;
            StrName.Text = String.Empty;
            StrNum.Text = String.Empty;
            Zip.Content = String.Empty;
            Dept.Content = String.Empty;
        }
    }
}
