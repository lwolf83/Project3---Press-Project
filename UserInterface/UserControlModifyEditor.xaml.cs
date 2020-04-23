using Project_3___Press_Project;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace UserInterface
{

    public partial class UserControlModifyEditor : UserControl
    {
        private Editor SelectedEditor { get; set; }
        private JournalFilter JournalFilter { get; set; }

        private EditorAddModify EditorAddModify { get; set; }

        public UserControlModifyEditor()
        {
            EditorAddModify = new EditorAddModify();
            JournalFilter = new JournalFilter();
            InitializeComponent();
            EditorList.ItemsSource = EditorAddModify.Editors;
            NewspaperList.ItemsSource = JournalFilter.Newspaperlist;
        }        

        public void OnEditorChanged(object sender, SelectionChangedEventArgs args)
        {
            SelectedEditor = (Editor)(sender as ComboBox).SelectedItem;
            EditorName_textBox.Text = SelectedEditor.Name;
            EditorName_textBox.Focus();
            EditorName_textBox.Select(EditorName_textBox.Text.Length, 0);
            Message_TextBlock.Text = String.Empty;

            NewspaperList.ItemsSource = JournalFilter.GetNewspaperEditor(SelectedEditor);
        }

        private void txtEditorName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Save_Btn(sender, e);
            }
        }

        private void Save_Btn(object sender, RoutedEventArgs e)
        {
            bool nameExist = EditorAddModify.Editors.Any(n => n.Name.Equals(EditorName_textBox.Text));

            if (EditorName_textBox.Text.Equals(String.Empty))
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
                Message_TextBlock.Text = "Please Fill the Case";
            }
            else if (SelectedEditor.Name.Equals(EditorName_textBox.Text))
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
                Message_TextBlock.Text = "No Changes Have Occured";
            }
            else if (nameExist)
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
                Message_TextBlock.Text = "This Name Already Exists";
            }
            else
            {
                EditorAddModify.ModifyEditor(SelectedEditor, EditorName_textBox.Text);
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Green);
                Message_TextBlock.Text = "Editor Has Succefully Modified";
            }
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            Message_TextBlock.Text = String.Empty;
        }
    }
}