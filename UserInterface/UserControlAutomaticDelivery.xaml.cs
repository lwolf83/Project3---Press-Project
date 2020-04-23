using System;
using System.Collections.Generic;
using System.Linq;
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
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlAutomaticDelivery.xaml
    /// </summary>
    public partial class UserControlAutomaticDelivery : UserControl
    {
        public UserControlAutomaticDelivery()
        {
            InitializeComponent();
            allShop_ListBox.ItemsSource = ShopsLoader.FromUser(UserSingleton.GetInstance.User).ToList();
            selectedShop_ListBox.ItemsSource = new List<Shop>();
        }

        private void CreateAutomaticDelivery_Btn(object sender, RoutedEventArgs e)
        {
            if(areAllFieldsWellCompleted())
            {
                int newspaperQuantity = Convert.ToInt32(QuantityTextBox.Text);
                DateTime startindDate = Convert.ToDateTime(StartDate_DatePicker.SelectedDate);
                DateTime endingDate = Convert.ToDateTime(EndDate_DatePicker.SelectedDate);
                UserShop userShop = UserShopsLoader.GetUserShopFromUser(UserSingleton.GetInstance.User);

                Newspaper selectedNewspaper = (Newspaper) Newspaper_ListView.Displaying_ListView.SelectedItem;
                AutomaticOrder ao = new AutomaticOrder();

                List<Shop> selectedShop = (List<Shop>)selectedShop_ListBox.ItemsSource;
                foreach(Shop shop in selectedShop)
                {
                    ao.CreateAutomaticOrderInDB(shop, selectedNewspaper, startindDate, endingDate, newspaperQuantity);
                }

                DialogBox.OK("Automatic orders has been recorded", "Info");
            }
            else
            {
                DialogBox.OK("Check input fields.", "Input error");
            }
        }

        private Boolean areAllFieldsWellCompleted()
        {
            bool isStartingDateOK = false;
            if((StartDate_DatePicker.SelectedDate != null)
                && (EndDate_DatePicker.SelectedDate != null)
                && ((DateTime)StartDate_DatePicker.SelectedDate >= DateTime.Today)
                && (InputChecker.CheckDates((DateTime)StartDate_DatePicker.SelectedDate, (DateTime)EndDate_DatePicker.SelectedDate)))
            {
                isStartingDateOK = true;
            }

            bool isSelectedShops = false;
            if(selectedShop_ListBox.Items.Count > 0)
            {
                isSelectedShops = true;
            }

            int newspaperQuantity = 0;
            Int32.TryParse(QuantityTextBox.Text, out newspaperQuantity);
            bool isQuantityOK = checkQuantity(newspaperQuantity);
            if(isStartingDateOK && isQuantityOK && (Newspaper_ListView.Displaying_ListView.SelectedItem != null) && isSelectedShops)
            {
                return true;
            }
            return false;
        }

        private Boolean checkQuantity(int quantity)
        {
            if (quantity <= 250 && quantity > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PreviewInputOnlyNumbersCaracters(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyNumbersCaracters(sender, e);
        }

        private void AddAllShop_Click(object sender, RoutedEventArgs e)
        {
            List<Shop> allShop = (List<Shop>)allShop_ListBox.ItemsSource;
            List<Shop> selectedShop = (List<Shop>)selectedShop_ListBox.ItemsSource;

            selectedShop.AddRange(allShop);
            allShop.Clear();

            RefreshListboxShops(allShop, selectedShop);

        }

        private void RemoveAllShop_Click(object sender, RoutedEventArgs e)
        {
            List<Shop> allShop = (List<Shop>)allShop_ListBox.ItemsSource;
            List<Shop> selectedShop = (List<Shop>)selectedShop_ListBox.ItemsSource;

            allShop.AddRange(selectedShop);
            selectedShop.Clear();

            RefreshListboxShops(allShop, selectedShop);
        }

        private void AddSelectedShop_Click(object sender, RoutedEventArgs e)
        {
            List<Shop> allShop = (List<Shop>)allShop_ListBox.ItemsSource;
            List<Shop> selectedShop = allShop_ListBox.SelectedItems.Cast<Shop>().ToList();
            List<Shop> currentShops = (List<Shop>)selectedShop_ListBox.ItemsSource;

            currentShops.AddRange(selectedShop);
            allShop.RemoveAll(x => selectedShop.Contains(x));

            RefreshListboxShops(allShop, currentShops);

        }

        private void RemoveSelectedShop_Click(object sender, RoutedEventArgs e)
        {
            List<Shop> allShop = (List<Shop>)allShop_ListBox.ItemsSource;
            List<Shop> selectedShop = selectedShop_ListBox.SelectedItems.Cast<Shop>().ToList();
            List<Shop> currentShops = (List<Shop>)selectedShop_ListBox.ItemsSource;

            allShop.AddRange(selectedShop);
            currentShops.RemoveAll(x => selectedShop.Contains(x));

            RefreshListboxShops(allShop, currentShops);
        }

        private void RefreshListboxShops(List<Shop> allShop, List<Shop> selectedShop)
        {
            allShop_ListBox.ItemsSource = allShop;
            allShop_ListBox.Items.Refresh();

            selectedShop_ListBox.ItemsSource = selectedShop;
            selectedShop_ListBox.Items.Refresh();
        }
    }
}
