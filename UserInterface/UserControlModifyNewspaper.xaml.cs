using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using Project_3___Press_Project;
using System.Text.RegularExpressions;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlModifyNewspaper.xaml
    /// </summary>
    public partial class UserControlModifyNewspaper : UserControl
    {
        private readonly NewspaperRepository _newspaperRepository;

        public UserControlModifyNewspaper()
        {
            _newspaperRepository = new NewspaperRepository();
            InitializeComponent();
        }

        private void ButtonFetchNewspaper_Click(object sender, RoutedEventArgs e)
        {
            if (NewsName.Text == String.Empty)
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Provide an existing newspaper name !";
                MessageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                Newspaper newspaper = _newspaperRepository.FindByName(NewsName.Text);
                if (newspaper != null)
                {
                    eanNum.Text = newspaper.EAN13;
                    Price.Text = Convert.ToString(newspaper.Price);
                    Period.Text = Convert.ToString(newspaper.Periodicity);
                    Editor.Text = newspaper.Editor.Name;
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
                Newspaper newspaper = _newspaperRepository.FindByEAN13(eanNum.Text);
                newspaper.Name = NewsName.Text;
                newspaper.Price = Decimal.Parse(Price.Text);
                newspaper.Periodicity = int.Parse(Period.Text);
                _newspaperRepository.Update(newspaper);
                Reset();
                MessageGrid.Background = Brushes.LightGreen;
                Message.Content = "Successfully modified!";
                MessageGrid.Visibility = Visibility.Visible;
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
