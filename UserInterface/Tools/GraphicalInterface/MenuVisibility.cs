using System.Windows;
using System.Windows.Controls;

namespace UserInterface
{
    public static class MenuVisibility
    {
        public static void ShowMenu()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            DockPanel menuNav = (DockPanel)mainWindow.FindName("menuNav");
            menuNav.Visibility = Visibility.Visible;
        }

        public static void HideMenu()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            DockPanel menuNav = (DockPanel)mainWindow.FindName("menuNav");
            menuNav.Visibility = Visibility.Hidden;
        }

    }
}
