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
        }

        private void CreateAutomaticDelivery_Btn(object sender, RoutedEventArgs e)
        {
            if(areAllFieldsWellCompleted())
            {
                int newspaperQuantity = Convert.ToInt32(QuantityTextBox.Text);
                DateTime startindDate = Convert.ToDateTime(StartDate_DatePicker.SelectedDate);
                DateTime endingDate = Convert.ToDateTime(EndDate_DatePicker.SelectedDate);
                UserShop userShop = UserShop.GetUserShopFromUser(UserSingleton.GetInstance.User);

                Newspaper selectedNewspaper = (Newspaper) Newspaper_ListView.Displaying_ListView.SelectedItem;
                AutomaticOrder ao = new AutomaticOrder();
                ao.CreateAutomaticOrderInDB(userShop, selectedNewspaper, startindDate, endingDate, newspaperQuantity);

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

            int newspaperQuantity = 0;
            Int32.TryParse(QuantityTextBox.Text, out newspaperQuantity);
            bool isQuantityOK = checkQuantity(newspaperQuantity);
            if(isStartingDateOK && isQuantityOK && (Newspaper_ListView.Displaying_ListView.SelectedItem != null))
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
    }
}
