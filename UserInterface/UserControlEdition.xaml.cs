using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class UserControlEdition : UserControl
    {
        public UserControlEdition()
        {
            InitializeComponent();
            Catalog catalog = new Catalog();
            Catalogs = catalog.GetAllCatalogs();
            EditorFilteringSelection.ItemsSource = catalog.GetEditorsHavingCatalogs(Catalogs);
            NewspaperNameFilteringSelection.ItemsSource = catalog.GetNewspapersHavingCatalogs(Catalogs);
            CatalogsDisplaying_ListView.ItemsSource = Catalogs;
        }
        List<Catalog> Catalogs { get; set; }

        public List<Catalog> GetCatalogs()
        {
            List<Catalog> catalogs = new List<Catalog>();
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
                Catalog catalog = new Catalog();
                CatalogsDisplaying_ListView.ItemsSource = catalog.GetCatalogsFromAnEditor(Catalogs, selectedEditor);
                NewspaperNameFilteringSelection.SelectedIndex = -1;
            }
        }

        private void OnNewspapersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NewspaperNameFilteringSelection.SelectedIndex != -1)
            {
                Newspaper selectedNewspaper = (Newspaper)(sender as ComboBox).SelectedItem;
                Catalog catalog = new Catalog();
                CatalogsDisplaying_ListView.ItemsSource = catalog.GetCatalogsFromANewspaper(Catalogs, selectedNewspaper);
                EditorFilteringSelection.SelectedIndex = -1;
            }
        }

        private void FilterByDates_Btn(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            bool isFirstDateFormatOk = InputChecker.CheckDateFormatFromString(FirstDateUserInput.Text);
            bool isLastDateFormatOk = InputChecker.CheckDateFormatFromString(LastDateUserInput.Text);

            if (isFirstDateFormatOk && isLastDateFormatOk)
            {
                DateTime firstDate = DateTime.ParseExact(FirstDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime lastDate = DateTime.ParseExact(LastDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if(InputChecker.CheckDates(firstDate, lastDate))
                {
                    CatalogsDisplaying_ListView.ItemsSource = catalog.GetCatalogsFromDates(Catalogs, firstDate, lastDate);
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
            Catalog catalog = new Catalog();
            Newspaper np = new Newspaper();
            if(np.IsEAN13Registered(ean13))
            {
                CatalogsDisplaying_ListView.ItemsSource = catalog.GetCatalogsFromEAN13(Catalogs, ean13);
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
            Catalog catalog = new Catalog();
            Catalogs = catalog.GetAllCatalogs();
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
