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
    public partial class UserControlAddNewspaper : UserControl
    {
        public UserControlAddNewspaper()
        {
            InitializeComponent();
            EditorNameFilteringSelection_comboBox.ItemsSource = Editor.GetAllEditors();
        }
        public Editor Editor = new Editor();
        
        public void ConfirmNewspaperAddition_Btn(object sender, RoutedEventArgs e)
        {
            if(CheckUserFieldsAreComplete(sender, e) == true)
            {
                if((CheckPublicationDateFormat(NewspaperFirstPublicationDate_textBox.Text) == true)
                    && CheckPriceFormat(NewspaperPrice_textBox.Text) == true
                    && CheckPeriodicityLength(NewspaperPeriodicity_textBox.Text) == true)
                {
                    CreateNewspaper(sender, e);
                    string msgtext = "Newspaper has been correctly added.";
                    string txt = "Newspaper addition";
                    bool answserOverwrite = DialogBox.OK(msgtext, txt);
                }
            }
        }

        public Newspaper CreateNewspaper(object sender, RoutedEventArgs e)
        {
            Newspaper newspaper = new Newspaper();
            using (var context = new PressContext())
            {
                Catalog catalog = CreateCatalog(sender, e);
                List<Catalog> catalogs = new List<Catalog>();
                catalogs.Add(catalog);
                newspaper.Name = NewspaperName_textBox.Text;
                newspaper.Periodicity = Convert.ToInt32(NewspaperPeriodicity_textBox.Text);
                newspaper.EAN13 = NewspaperEAN13_textBox.Text;
                newspaper.Price = Convert.ToDecimal(NewspaperPrice_textBox.Text);
                newspaper.Catalogs = catalogs;
                Editor selectedEditor = (Editor)EditorNameFilteringSelection_comboBox.SelectedItem;
                var editor = (from i in context.Editors
                              where i.EditorId == selectedEditor.EditorId
                              select i).First();
                newspaper.Editor = editor;
                context.Add(catalog);
                context.Add(newspaper);
                context.SaveChanges();
            }
            return newspaper;
        }

        public Catalog CreateCatalog(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            string publicationDateString = NewspaperFirstPublicationDate_textBox.Text;
            catalog.PublicationDate = DateTime.ParseExact(publicationDateString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return catalog;
        }

        public bool CheckPublicationDateFormat(string publicationDateString)
        {
            try
            {
                DateTime publicationDate = DateTime.ParseExact(publicationDateString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (publicationDate >= DateTime.Today)
                {
                    return true;
                }
                else
                {
                    string msgtext = "Date should be superior or equal to today.";
                    string txt = "Invalid date";
                    bool answserOverwrite = DialogBox.OK(msgtext, txt);
                    NewspaperFirstPublicationDate_textBox.Background = Brushes.IndianRed;
                    return false;
                }
            }
            catch
            {
                string msgtext = "Allows only dd/MM/yyyy format.";
                string txt = "Invalid date format";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
                NewspaperFirstPublicationDate_textBox.Background = Brushes.IndianRed;
                return false;
            }
        }

        public bool CheckPriceFormat(string newspaperPriceString)
        {
            try
            {
                decimal newspaperPrice = Convert.ToDecimal(newspaperPriceString);
                string[] a = newspaperPriceString.Split(new char[] { ',' });
                int decimals = a[1].Length;

                if (newspaperPrice > 0 && (decimals<=2))
                {
                    return true;
                }
                else
                {
                    string msgtext = "Invalid price format.";
                    string txt = "Invalid input";
                    bool answserOverwrite = DialogBox.OK(msgtext, txt);
                    NewspaperPrice_textBox.Background = Brushes.IndianRed;
                    return false;
                }
            }
            catch
            {
                string msgtext = "Invalid price.";
                string txt = "Invalid input";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
                NewspaperPrice_textBox.Background = Brushes.IndianRed;
                return false;
            }
        }

        public bool CheckPeriodicityLength(string periodicityString)
        {
            if(Convert.ToInt32(periodicityString) <=366)
            {
                return true;
            }
            else
            {
                string msgtext = "Periodicity should be lower than a year.";
                string txt = "Invalid input";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
                NewspaperPeriodicity_textBox.Background = Brushes.IndianRed;
                return false;
            }
        }

        public bool CheckUserFieldsAreComplete(object sender, RoutedEventArgs e)
        {
            string newspaperName = NewspaperName_textBox.Text;
            string periodicity = NewspaperPeriodicity_textBox.Text;
            string ean13 = NewspaperEAN13_textBox.Text;
            string price = NewspaperPrice_textBox.Text;
            if((newspaperName != string.Empty) 
                && (periodicity != string.Empty) 
                && (ean13 != string.Empty) 
                && (price != string.Empty) 
                && (!string.IsNullOrEmpty(EditorNameFilteringSelection_comboBox.Text)))
            {
                return true;
            }
            else
            {
                string msgtext = "Please fulfill every inputs.";
                string txt = "Invalid inputs";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
                return false;
            }
        }

        private void CheckNewspaperPrice_textBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckInputHasOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CheckDateInputCaracters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}