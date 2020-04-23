using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            UserControlSetter.SetGridMain(GridMain, "ItemLogin");
        }
                
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemLogout");
        }

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemClose");                     
        }

        public void ButtonSeeEditors_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemEditors");
        }

        public void ButtonSeeOrder_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemOrder");
        }

        public void ButtonSeeShop_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemShop");
        }

        public void ButtonSeeNewspapers_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemNewspaper");
        }

        public void ButtonSeeStock_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemStock");
        }

        public void ButtonAddNewspapers_click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemAddNewspapers");
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            if(!UserSingleton.GetInstance.IsAuthenticated)
            {
                UserControlSetter.SetGridMain(GridMain, "ItemLogin");
            }
            else
            {
                string menuAction = ((ListViewItem)((ListView)sender).SelectedItem).Name;
                UserControlSetter.SetGridMain(GridMain, menuAction);
            }            
        }

        private void ButtonAddShops_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "AddShop");
        }

        public void ButtonAddEditors_click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemAddEditors");
        }
        public void ButtonModifyEditors_click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemModifyEditors");
        }

        private void ButtonSeeEditions_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemEditions");
        }

        private void ButtonSeeCatalog_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemCatalog");

        }

        private void ButtonAddAutomaticDelivery_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemAutomaticDelivery");
        }

        private void ButtonModifyNewspaper_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemModifyNewspaper");
        }

        private void ButtonModifyShop_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemModifyShop");
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "MainMenu");
        }
    }
}

