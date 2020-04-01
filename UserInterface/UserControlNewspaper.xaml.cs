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
    public partial class UserControlNewspaper : UserControl
    {
        public UserControlNewspaper()
        {
            InitializeComponent();
            EditorNameFilteringSelection_comboBox.ItemsSource = GetAllEditors();
        }

        public List<Editor> GetAllEditors()
        {
            List<Editor> editors = new List<Editor>();
            using (var context = new PressContext())
            {
                editors = (from i in context.Editors
                            select i).ToList();

            }
            return editors;
        }

        public void ConfirmNewspaperAddition_Btn(object sender, RoutedEventArgs e)
        {
            if(CheckUserInputs(sender, e) == true)
            {
                CreateNewspaper(sender, e);
                NewspaperAdditionOk_Display.Visibility = Visibility.Visible;
            }
            else
            {
                WarningCheckInputs_Display.Visibility = Visibility.Visible;
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
            try
            {
                catalog.PublicationDate = DateTime.ParseExact(publicationDateString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return catalog;
            }
            catch (Exception f)
            {
                string message = "Invalid date format \n allows only dd/MM/yyyy format " + f;
                ErrorNewspaperAddition_TextBlock.Text = message;
                ErrorInput_Display.Visibility = Visibility.Visible;
                throw new Exception();
            }
        }
        
        public bool CheckUserInputs(object sender, RoutedEventArgs e)
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
                return false;
            }
        }

        private void AddNewspaper_Btn(object sender, RoutedEventArgs e)
        {
            AddNewspaper_Grid.Visibility = Visibility.Visible;
            ModifyNewspaperlv_grid.Visibility = Visibility.Collapsed;
        }

        private void ModifyNewspaper_Btn(object sender, RoutedEventArgs e)
        {
            AddNewspaper_Grid.Visibility = Visibility.Collapsed;
            ModifyNewspaperlv_grid.Visibility = Visibility.Visible;
        }

        private void WarningCheckInputs_Btn(object sender, RoutedEventArgs e)
        {
            WarningCheckInputs_Display.Visibility = Visibility.Collapsed;
        }

        private void NewspaperAddedWell_Btn(object sender, RoutedEventArgs e)
        {
            NewspaperAdditionOk_Display.Visibility = Visibility.Collapsed;
        }

        private void ErrorInput_Btn(object sender, RoutedEventArgs e)
        {
            ErrorInput_Display.Visibility = Visibility.Collapsed;
        }

        private void CheckNewspaperPrice_textBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
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

        private void ModifySelectedNewspaper_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        
    }
}