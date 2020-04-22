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
using System.Text.RegularExpressions;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlModifyNewspaper.xaml
    /// </summary>
    public partial class UserControlModifyNewspaper : UserControl
    {
        
        public IEnumerable<Newspaper> AllNespapers { get; set; }

        public UserControlModifyNewspaper()
        {
            InitializeComponent();
            AllNespapers = Newspaper.GetNewspaperData();
        }

        private void ButtonFetchNewspaper_Click(object sender, RoutedEventArgs e)
        {
            string newspaperName = NewsName.Text;
            if (newspaperName == String.Empty)
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Provide an existing newspaper name !";
                MessageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                bool validName = AllNespapers.Where(n => n.Name.Equals(newspaperName)).Any();
                if (validName)
                {
                    Newspaper np = AllNespapers.Where(n => n.Name.Equals(newspaperName)).First();
                    eanNum.Text = np.EAN13;
                    Price.Text = Convert.ToString(np.Price);
                    Period.Text = Convert.ToString(np.Periodicity);
                    Editor.Text = np.Editor.Name;
                }
                else
                {
                    MessageGrid.Background = Brushes.IndianRed;
                    Message.Content = "Non-valid Newspaper name!";
                    MessageGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            bool emptyFields = false;
            if (NewsName.Text == String.Empty ||
                Price.Text == String.Empty ||
                Period.Text == String.Empty ||
                Editor.Text == String.Empty ||
                eanNum.Text == String.Empty
                )
            {
                emptyFields = true;
            }
            
            if (emptyFields)
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Fill up all the fields !";
                MessageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                string EAN13 = eanNum.Text;
                string newName = NewsName.Text;
                decimal newprice = Decimal.Parse(Price.Text);
                int newPeriodicity = int.Parse(Period.Text);
                bool modifOk = Newspaper.ModifyNewspaper(EAN13, newName, newprice, newPeriodicity);
                if (modifOk)
                {
                    Reset();
                    MessageGrid.Background = Brushes.LightGreen;
                    Message.Content = "Successfully modified!";
                    MessageGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageGrid.Background = Brushes.IndianRed;
                    Message.Content = "Double-check your data..";
                    MessageGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Hidden;
        }

        private void Reset()
        {
            NewsName.Text = String.Empty;
            Price.Text = String.Empty;
            Period.Text = String.Empty;
            Editor.Text = String.Empty;
            eanNum.Text = String.Empty;
        }

        private void CheckInputHasOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckInputIsDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
