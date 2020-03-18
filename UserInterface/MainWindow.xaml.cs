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

//using MessageBox = System.Windows.Forms.MessageBox;

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool notConnected { get; set; } = true;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            notConnected = true;
        }

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {/*
            string message = "Do you want to quit the application";
            string title = "Close Application";
            System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.YesNo;
            System.Windows.Forms.DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Current.Shutdown();
            }        */
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            
            if(notConnected)
            {
                usc = new UserControlLogin();
                GridMain.Children.Add(usc);
            }
            else
            {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {
                    case "ItemShop":
                        usc = new UserControlShop();
                        GridMain.Children.Add(usc);
                        break;
                    case "ItemEditor":
                        usc = new UserControlEditor();
                        GridMain.Children.Add(usc);
                        break;
                    case "ItemStat":
                        usc = new UserControlStat();
                        GridMain.Children.Add(usc);
                        break;
                    case "ItemAccount":
                        usc = new UserControlAccount();
                        GridMain.Children.Add(usc);
                        break;
                    case "ItemSetting":
                        usc = new UserControlSetting();
                        GridMain.Children.Add(usc);
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}
