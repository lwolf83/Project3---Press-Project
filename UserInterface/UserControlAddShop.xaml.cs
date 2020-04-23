using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlAddShop.xaml
    /// </summary>
    public partial class UserControlAddShop : UserControl
    {   
        public UserControlAddShop()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFormFilled())
            {
                string shopName = ShopName.Text;
                string streetNumber = StrNum.Text;
                string streetName = StrName.Text;
                string cityName = Autocompletor.City.Text;
                bool shopCreated = ShopAdderDb.AddShop(shopName, streetNumber, streetName, cityName);
                if (shopCreated)
                {
                    Reset();
                    MessageGrid.Background = Brushes.LightGreen;
                    Message.Content = "Shop is successfully created !";
                    MessageGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    Reset();
                    MessageGrid.Background = Brushes.IndianRed;
                    Message.Content = "Shop creation failed. Please enter a valid city";
                    MessageGrid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Please fill up all the required fields !";
                MessageGrid.Visibility = Visibility.Visible;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        
        private void Reset()
        {
            ShopName.Text = String.Empty;
            Zip.Content = String.Empty;
            Dept.Content = String.Empty;
            StrName.Text = String.Empty;
            StrNum.Text = String.Empty;
            Autocompletor.City.Text = String.Empty;
        }

        private bool CheckFormFilled()
        {
            bool isFilled = !(ShopName.Text == String.Empty) && !(Autocompletor.City.Text == String.Empty)
                                    && !(StrName.Text == String.Empty) && !(StrNum.Text == String.Empty);
            return isFilled;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Hidden;
        }
    }
}
