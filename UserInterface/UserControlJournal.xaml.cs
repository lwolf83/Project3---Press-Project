using System.Windows;
using System.Windows.Controls;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlJournal.xaml
    /// </summary>
    public partial class UserControlJournal : UserControl
    {
        public UserControlJournal()
        {            
            InitializeComponent();
        }

        private void Edition_Click(object sender, RoutedEventArgs e)
        {
            UserControlEdition uscEdition = new UserControlEdition();
            this.Content = uscEdition;
        }

        private void AddNewspaper_Click(object sender, RoutedEventArgs e)
        {
            UserControlAddNewspaper uscAddNewspaper = new UserControlAddNewspaper();
            this.Content = uscAddNewspaper;
        }
    }  
}