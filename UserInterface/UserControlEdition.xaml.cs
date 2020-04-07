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
            EditorFilteringSelection.ItemsSource = catalog.GetEditorsHavingEditions(Catalogs);
            NewspaperNameFilteringSelection.ItemsSource = catalog.GetNewspapersHavingEditions(Catalogs);
            EditionsDisplaying_ListView.ItemsSource = Catalogs;
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
                Catalogs = catalog.GetCatalogsFromAnEditor(Catalogs, selectedEditor);
                EditionsDisplaying_ListView.ItemsSource = Catalogs;
            }
        }

        private void OnNewspapersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(NewspaperNameFilteringSelection.SelectedIndex != -1)
            {
                Newspaper selectedNewspaper = (Newspaper)(sender as ComboBox).SelectedItem;
                Catalog catalog = new Catalog();
                Catalogs = catalog.GetCatalogsFromANewspaper(Catalogs, selectedNewspaper);
                EditionsDisplaying_ListView.ItemsSource = Catalogs;
            }
        }

        private void FilterByDates_Btn(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            bool isFirstDateFormatOk = catalog.CheckDateFormatFromString(FirstDateUserInput.Text);
            bool isLastDateFormatOk = catalog.CheckDateFormatFromString(LastDateUserInput.Text);
            
            if(isFirstDateFormatOk && isLastDateFormatOk)
            {
                DateTime firstDate = DateTime.ParseExact(FirstDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime lastDate = DateTime.ParseExact(LastDateUserInput.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if(catalog.CheckDates(firstDate, lastDate))
                {
                    Catalogs = catalog.GetEditionsFromDates(Catalogs, firstDate, lastDate);
                    EditionsDisplaying_ListView.ItemsSource = Catalogs;
                }
                else
                {
                    string msgtext = "Wrong dates input.";
                    string txt = "Invalid dates";
                    bool answserOverwrite = DialogBox.OK(msgtext, txt);
                }
            }
            else
            {
                string msgtext = "Allows only dd/MM/yyyy format.";
                string txt = "Invalid date format";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
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
                Catalogs = catalog.GetEditionsFromEAN13(Catalogs, ean13);
                EditionsDisplaying_ListView.ItemsSource = Catalogs;
            }
            else
            {
                string msgtext = $"No newspaper with ean13 {ean13} recorded.";
                string txt = "Invalid input";
                bool answserOverwrite = DialogBox.OK(msgtext, txt);
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
            EditionsDisplaying_ListView.ItemsSource = Catalogs;
        }

        private void ViewEditionDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Display Edition Details
        }
    }
}
