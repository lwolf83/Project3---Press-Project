using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;

namespace UserInterface
{
    public partial class UserControlLogout : UserControl
    {
        public UserControlLogout()
        {
            InitializeComponent();
            
            if (UserSingleton.GetInstance.IsAuthenticated is false)
            {
                MenuVisibility.HideMenu();
                UserControlSetter.SetGridMain(this, "ItemLogin");
            }           
        }

        private void ButtonLogoutApplicationYes_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton.Disconnect();
            MenuVisibility.HideMenu();
            UserControlSetter.SetGridMain(this, "ItemLogin");
        }

        private void ButtonLogoutApplicationNo_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(this, "ItemShop");
        }
    }
}
