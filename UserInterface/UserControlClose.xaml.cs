using Project_3___Press_Project;
using System.Windows;
using System.Windows.Controls;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlClose.xaml
    /// </summary>
    public partial class UserControlClose : UserControl
    {
        public UserControlClose()
        {
            InitializeComponent();
        }

        private void ButtonCloseApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonCloseApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            if (UserSingleton.Instance.IsAuthenticated is true)
            {
                UserControl usc = new UserControlShop();
                this.Content = usc;
            }
            else
            {
                UserControl usc = new UserControlLogin();
                this.Content = usc;
            }
        }
    }
}
