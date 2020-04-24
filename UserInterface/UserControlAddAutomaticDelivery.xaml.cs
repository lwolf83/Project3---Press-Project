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
    public partial class UserControlAddAutomaticDelivery : UserControl
    {
        private AutomaticOrder _automaticOrder;
        public UserControlAddAutomaticDelivery()
        {
            InitializeComponent();
            allShop_ListBox.ItemsSource = ShopsLoader.FromUser(UserSingleton.GetInstance.User).ToList();
            selectedShop_ListBox.ItemsSource = new List<Shop>();
        }

        public UserControlAddAutomaticDelivery(AutomaticOrder automaticOrder)
        {
            InitializeComponent();
            _automaticOrder = automaticOrder;
            allShop_ListBox.ItemsSource = ShopsLoader.FromUser(UserSingleton.GetInstance.User).ToList();
            selectedShop_ListBox.ItemsSource = new List<Shop>() { automaticOrder.Shop };

            QuantityTextBox.Text = automaticOrder.Quantity.ToString();

            StartDate_DatePicker.SelectedDate = automaticOrder.StartingDate;
            StartDate_DatePicker.Text = automaticOrder.StartingDate.ToString();

            EndDate_DatePicker.SelectedDate = automaticOrder.EndDate;
            EndDate_DatePicker.Text = automaticOrder.EndDate.ToString();

            Create_Button.Visibility = Visibility.Hidden;
            Update_Button.Visibility = Visibility.Visible;
            Back_Button.Visibility = Visibility.Visible;

            SelectShop_GroupBox.IsEnabled = false;
            Newspaper_ListView.Displaying_ListView.ItemsSource = new List<Newspaper>() { automaticOrder.Newspaper };;
            Newspaper_ListView.Displaying_ListView.SelectedItem = automaticOrder.Newspaper;
            Newspaper_ListView.IsEnabled = false;
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

        private void UpdateAutomaticDelivery_Btn(object sender, RoutedEventArgs e)
        {
            if (areAllFieldsWellCompleted())
            {
                int newspaperQuantity = Convert.ToInt32(QuantityTextBox.Text);
                DateTime startindDate = Convert.ToDateTime(StartDate_DatePicker.SelectedDate);
                DateTime endingDate = Convert.ToDateTime(EndDate_DatePicker.SelectedDate);

                _automaticOrder.StartingDate = startindDate;
                _automaticOrder.Quantity = newspaperQuantity;
                _automaticOrder.EndDate = endingDate;
                _automaticOrder.SaveInDB();

                DialogBox.OK("Update done!", "Info");
                UserControlSetter.SetGridMain(this, "ItemAutomaticDelivery");
            }
            else
            {
                DialogBox.OK("Check input fields.", "Input error");
            }
        }

        private void BackAutomaticDelivery_Btn(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(this, "ItemAutomaticDelivery");
        }

        private Boolean areAllFieldsWellCompleted()
        {
            bool isStartingDateOK = false;
            if(StartDate_DatePicker.SelectedDate != null && EndDate_DatePicker.SelectedDate != null)
            {
                if ((DateTime)StartDate_DatePicker.SelectedDate >= DateTime.Today)
                {
                    if (InputChecker.CheckDates((DateTime)StartDate_DatePicker.SelectedDate, (DateTime)EndDate_DatePicker.SelectedDate))
                    {
                        isStartingDateOK = true;
                    }
                }
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
