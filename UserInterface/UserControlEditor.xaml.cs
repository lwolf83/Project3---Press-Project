using Project_3___Press_Project;
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

namespace UserInterface
{
    public partial class UserControlEditor : UserControl
    {
        private JournalFilter JournalFilter { get; set; }

        private EditorAddModify EditorAddModify { get; set; }

        public UserControlEditor()
        {
            EditorAddModify = new EditorAddModify();
            JournalFilter = new JournalFilter();
            InitializeComponent();
            EditorList.ItemsSource = EditorAddModify.Editors;
            NewspaperList.ItemsSource = JournalFilter.Newspaperlist;
        }

        public void OnEditorChanged(object sender, SelectionChangedEventArgs args)
        {
            NewspaperList.ItemsSource = JournalFilter.GetNewspaperEditor((Editor)EditorList.SelectedItem);
        }
    }
}