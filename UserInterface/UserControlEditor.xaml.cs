using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using System.Windows.Controls;

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