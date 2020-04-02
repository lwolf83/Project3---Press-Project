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

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlModifyNewspaper.xaml
    /// </summary>
    public partial class UserControlModifyNewspaper : UserControl
    {
        public string EAN13 { get; set; }
        public IEnumerable<Newspaper> AllNespapers { get; set; }

        public UserControlModifyNewspaper()
        {
            InitializeComponent();
            AllNespapers = NewspaperModifier.GetNewspaperData();
        }

        public void PrefillEAN13()
        {
            eanNum.Text = EAN13;
        }

        private void ButtonFetchNewspaper_Click(object sender, RoutedEventArgs e)
        {
            string EAN13 = eanNum.Text;
            if (EAN13 == String.Empty)
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Provide a EAN13 code first !";
                MessageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                bool validCode = AllNespapers.Where(n => n.EAN13.Equals(EAN13)).Any();
                if (validCode)
                {
                    Newspaper np = AllNespapers.Where(n => n.EAN13.Equals(EAN13)).First();
                    NewsName.Text = np.Name;
                    Price.Text = Convert.ToString(np.Price);
                    Period.Text = Convert.ToString(np.Periodicity);
                    Editor.Text = np.Editor.Name;
                }
                else
                {
                    MessageGrid.Background = Brushes.IndianRed;
                    Message.Content = "Non-valid EAN13 code!";
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
                bool modifOk = NewspaperModifier.ModifyNewspaper(EAN13, newName, newprice, newPeriodicity);
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
    }
}
