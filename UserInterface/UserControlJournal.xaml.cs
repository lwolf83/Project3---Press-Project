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
    /// <summary>
    /// Interaction logic for UserControlJournal.xaml
    /// </summary>
    public partial class UserControlJournal : UserControl
    {
        JournalFilter JournalFilter { get; set; }

        public UserControlJournal()
        {            
            InitializeComponent();
            JournalFilter = new JournalFilter();

            Displaying_ListView.ItemsSource = JournalFilter.Newspaperlist;
            FilteringEditors.ItemsSource = JournalFilter.DistinctEditors;
            FilteringPeriodicity.ItemsSource = JournalFilter.DistinctPeriodicity;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Displaying_ListView.ItemsSource = JournalFilter.Newspaperlist;
            FilteringEditors.Text = String.Empty;
            FilteringPeriodicity.Text = String.Empty;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Newspaper selectedEditor = (Newspaper)FilteringEditors.SelectedItem;
            Newspaper selectedPeriodicity = (Newspaper)FilteringPeriodicity.SelectedItem;

            Displaying_ListView.ItemsSource = JournalFilter.GetNewspaper(selectedEditor, selectedPeriodicity);
        }

        private void Edition_Click(object sender, RoutedEventArgs e)
        {
            UserControlEdition uscEdition = new UserControlEdition();
            this.Content = uscEdition;
        }
    }  
}