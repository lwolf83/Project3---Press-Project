using System;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;

namespace UserInterface
{
    
    /// <summary>
    /// Logique d'interaction pour UserControlNewspapersView.xaml
    /// </summary>
    public partial class UserControlNewspapersView : UserControl
    {
        JournalFilter JournalFilter { get; set; }

        public UserControlNewspapersView()
        {
            InitializeComponent();
            JournalFilter = new JournalFilter();

            Displaying_ListView.ItemsSource = JournalFilter.Newspaperlist;
            FilteringEditors.ItemsSource = JournalFilter.DistinctEditors;
            FilteringPeriodicity.ItemsSource = JournalFilter.DistinctPeriodicity;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Newspaper selectedEditor = (Newspaper)FilteringEditors.SelectedItem;
            Newspaper selectedPeriodicity = (Newspaper)FilteringPeriodicity.SelectedItem;

            Displaying_ListView.ItemsSource = JournalFilter.GetNewspaper(selectedEditor, selectedPeriodicity);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Displaying_ListView.ItemsSource = JournalFilter.Newspaperlist;
            FilteringEditors.Text = String.Empty;
            FilteringPeriodicity.Text = String.Empty;
        }
    }
}
