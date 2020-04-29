using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    public partial class UserControlEdition : UserControl
    {
        private readonly CatalogRepository _catalogRepository;
        private readonly NewspaperRepository _newspaperRepository;

        public UserControlEdition()
        {
            InitializeComponent();
            Catalogs = CatalogLoader.GetAll();
            EditorFilteringSelection.ItemsSource = _catalogRepository.GetEditorsHavingCatalogs(Catalogs);
            CatalogsDisplaying_ListView.ItemsSource = Catalogs;
        }
        List<Catalog> Catalogs { get; set; }

        public List<Catalog> GetCatalogs()
        {
            List<Catalog> catalogs;
            using (var context = new PressContext())
            {
                catalogs = (from c in context.Catalogs
                            select c).ToList();
            }
            return catalogs;
        }
        
        private void OnEditorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EditorFilteringSelection.SelectedIndex != -1)
            {
                Editor selectedEditor = (Editor)(sender as ComboBox).SelectedItem;
                List<Catalog> catalogs = _catalogRepository.GetCatalogsFromAnEditor(Catalogs, selectedEditor);
                CatalogsDisplaying_ListView.ItemsSource = catalogs;
                NewspaperNameFilteringSelection.ItemsSource = _catalogRepository.GetNewspapersHavingCatalogs(catalogs);
                NewspaperNameFilteringSelection.SelectedIndex = -1;
            }
        }

        private void OnNewspapersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NewspaperNameFilteringSelection.SelectedIndex != -1)
            {
                Newspaper selectedNewspaper = (Newspaper)(sender as ComboBox).SelectedItem;
                CatalogsDisplaying_ListView.ItemsSource = _catalogRepository.GetCatalogsFromANewspaper(Catalogs, selectedNewspaper);
            }
        }

        private void FilterByDates_Btn(object sender, RoutedEventArgs e)
        {
            bool isFirstDateFormatOk = InputChecker.CheckDateFormatFromString(FirstDateUserInput.Text);
            bool isLastDateFormatOk = InputChecker.CheckDateFormatFromString(LastDateUserInput.Text);

            if (isFirstDateFormatOk && isLastDateFormatOk)
            {
                DateTime firstDate = DateTime.ParseExact(FirstDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime lastDate = DateTime.ParseExact(LastDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if(InputChecker.CheckDates(firstDate, lastDate))
                {
                    CatalogsDisplaying_ListView.ItemsSource = _catalogRepository.GetCatalogsFromDates(Catalogs, firstDate, lastDate);
                }
                else
                {
                    string msgtext = "Wrong dates input.";
                    string txt = "Invalid dates";
                    DialogBox.OK(msgtext, txt);
                }
            }
            else
            {
                string msgtext = "Allows only dd/MM/yyyy format.";
                string txt = "Invalid date format";
                DialogBox.OK(msgtext, txt);
            }
        }

        private void EAN13FilterSelection_Btn(object sender, RoutedEventArgs e)
        {
            string ean13 = EAN13UserInput.Text;
            Reset();
            EAN13UserInput.Text = ean13;
            if(_newspaperRepository.EAN13Exists(ean13))
            {
                CatalogsDisplaying_ListView.ItemsSource = _catalogRepository.GetCatalogsFromEAN13(Catalogs, ean13);
            }
            else
            {
                string msgtext = $"No newspaper with ean13 {ean13} recorded.";
                string txt = "Invalid input";
                DialogBox.OK(msgtext, txt);
            }
        }

        private void ReinitializeCatalogList_Btn(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            EditorFilteringSelection.SelectedIndex = -1;
            NewspaperNameFilteringSelection.SelectedIndex = -1;
            FirstDateUserInput.Text = String.Empty;
            LastDateUserInput.Text = String.Empty;
            EAN13UserInput.Text = String.Empty;
            Catalogs = CatalogLoader.GetAll();
            CatalogsDisplaying_ListView.ItemsSource = Catalogs;
        }

        private void ViewEditionDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Display Edition Details
        }

        private void ControlDateInput(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyDateCaracters(sender, e);
        }

        private void ControlOnlyNumberInput(object sender, TextCompositionEventArgs e)
        {
            InputChecker.ControlInputOnlyNumbersCaracters(sender, e);
        }
    }
}
