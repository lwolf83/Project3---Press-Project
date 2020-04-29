using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Factory;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    public partial class UserControlAddNewspaper : UserControl
    {
        private readonly EditorRepository _editorRepository;
        private readonly NewspaperRepository _newspaperRepository;
        private readonly CatalogRepository _catalogRepository;

        public UserControlAddNewspaper()
        {
            InitializeComponent();
            EditorNameFilteringSelection_comboBox.ItemsSource = _editorRepository.GetAllEditors();
        }
        public Editor Editor = new Editor();
        
        public void ConfirmNewspaperAddition_Btn(object sender, RoutedEventArgs e)
        {
            if((CheckUserFieldsAreComplete(sender, e) == true) 
                && (CheckPublicationDateFormat(NewspaperFirstPublicationDate_textBox.Text) == true)
                && CheckPriceFormat(NewspaperPrice_textBox.Text) == true
                && CheckPeriodicityLength(NewspaperPeriodicity_textBox.Text) == true)
            {
                CreateNewspaper(sender, e);
            }
        }

        public Newspaper CreateNewspaper(object sender, RoutedEventArgs e)
        {
            DateTime publicationDate = DateTime.ParseExact(NewspaperFirstPublicationDate_textBox.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Catalog catalog = CatalogFactory.Create("First edition", publicationDate, null);

            string name = NewspaperName_textBox.Text;
            int periodicity = Convert.ToInt32(NewspaperPeriodicity_textBox.Text);
            string ean13 = NewspaperEAN13_textBox.Text;
            decimal price = Convert.ToDecimal(NewspaperPrice_textBox.Text);
            List<Catalog> catalogs = new List<Catalog>();
            catalogs.Add(catalog);
            Editor editor = EditorLoader.Get((Editor)EditorNameFilteringSelection_comboBox.SelectedItem);
            Newspaper newspaper = NewspaperFactory.Create(name, periodicity, ean13, price, catalogs, editor);

            if (!_newspaperRepository.Exists(newspaper))
            {
                _catalogRepository.Update(catalog);
                _newspaperRepository.Update(newspaper);
                string msgtext = "Newspaper has been correctly added.";
                string txt = "Newspaper addition";
                DialogBox.OK(msgtext, txt);
            }
            else
            {
                DialogBox.OK("Already recorded in DB", "Error");
            }
            return newspaper;
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
                DialogBox.OK(msgtext, txt);
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
                DialogBox.OK(msgtext, txt);
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
                DialogBox.OK(msgtext, txt);
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
                DialogBox.OK(msgtext, txt);
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