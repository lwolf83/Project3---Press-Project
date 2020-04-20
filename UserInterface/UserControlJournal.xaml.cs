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