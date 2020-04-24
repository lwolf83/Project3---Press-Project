using Project_3___Press_Project;
using System;
using System.Windows.Controls;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlMainMenu.xaml
    /// </summary>
    public partial class UserControlMainMenu : UserControl, IDisposable
    {
        private bool Disposed = false;

        public UserControlMainMenu()
        {
            Initialisator.OnApplicationStart();
            InitializeComponent();
            TextBlock_UserName.Text = "Welcome " + UserSingleton.GetInstance.User.Name;
            lv_OrderCatalogs.ItemsSource = UserSingleton.GetInstance.LatestOrderCatalogs;
            lv_LatestsCatalogs.ItemsSource = UserSingleton.GetInstance.LatestCatalogs;
            
        }

        public void Dispose()
        {
            if(!Disposed)
            {
                Initialisator.Finish();
                GC.SuppressFinalize(this);
                Disposed = true;
            }
        }

        private void GoShop(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControlSetter.SetGridMain(this, "ItemShop");
        }

        private void GoOrders(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControl usc = new UserControlOrder();
            this.Content = usc;
        }

        private void GoStocks(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControl usc = new UserControlStock();
            this.Content = usc;
        }

        private void GoNewspapers(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControl usc = new UserControlJournal();
            this.Content = usc;
        }

        private void GoEditors(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControl usc = new UserControlEditor();
            this.Content = usc;
        }

        private void Exiting(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserControl usc = new UserControlClose();
            this.Content = usc;
        }
    }
}
