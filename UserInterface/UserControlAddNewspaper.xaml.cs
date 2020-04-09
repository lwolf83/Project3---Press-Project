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
            if(InputChecker.IsDateValidAndHigherThanToday(publicationDateString))
            {
                return true;
            }
            else
            {
                string msgtext = "Allows only dd/MM/yyyy format \n with dates equal or higher than today.";
                string txt = "Invalid date format";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
                NewspaperFirstPublicationDate_textBox.Background = Brushes.IndianRed;
                return false;
            }
        }

        public bool CheckPriceFormat(string newspaperPriceString)
        {
            if(InputChecker.IsDecimalFormatValidWithTwoZeroAfterComa(newspaperPriceString))
            {
                return true;
            }
            else
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
            if(InputChecker.IsPeriodicityLowerThanReferencedDays(periodicityString, 366))
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
            List<String> inputs = new List<string>()
            {
                NewspaperName_textBox.Text,
                NewspaperPeriodicity_textBox.Text,
                NewspaperEAN13_textBox.Text,
                NewspaperPrice_textBox.Text,
                EditorNameFilteringSelection_comboBox.Text
            };

            if (InputChecker.AreAllStringFieldsComplete(inputs))
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

        private void ControlNewspaperPrice_textBox(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyDecimalCaracters(sender, e);
        }

        private void ControlInputHasOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyNumbersCaracters(sender, e);
        }

        private void ControlDateInputCaracters(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyDateCaracters(sender, e);
        }
    }
}