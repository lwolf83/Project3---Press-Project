using Project_3___Press_Project;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace UserInterface
{
    public partial class UserControlAddEditor : UserControl
    {
        public UserControlAddEditor()
        {
            InitializeComponent();
            Loaded += MyWpfControl_Load;
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
            string name = EditorName_textBox.Text;

            if (name.Equals(String.Empty))
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
                Message_TextBlock.Text = "Please fill every fields.";
                EditorName_textBox.Focus();
            }
            else
            {
                EditorAddModify editor = new EditorAddModify();
                bool existEditor = editor.CheckEditor(name);
                Message(existEditor);
            }                     
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            Reset();
            Message_TextBlock.Text = String.Empty;
        }

        private void Message(bool existEditor)
        {
            if(existEditor)
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Red);
                Message_TextBlock.Text = "Editor already exists";
                Reset();
            }
            else
            {
                Message_TextBlock.Foreground = new SolidColorBrush(Colors.Green);
                Message_TextBlock.Text = "Editor has been sucessfully added.";
                Reset();
            }
        }

        private void Reset()
        {
            EditorName_textBox.Text = String.Empty;
            EditorName_textBox.Focus();
        }

        private void MyWpfControl_Load(object sender, EventArgs e)
        {
            EditorName_textBox.Focus();
        }
    }
}