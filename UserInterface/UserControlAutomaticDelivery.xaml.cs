using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlAutomaticDelivery.xaml
    /// </summary>
    public partial class UserControlAutomaticDelivery : UserControl
    {
        public UserControlAutomaticDelivery()
        {
            InitializeComponent();
        }

        private void CreateAutomaticDelivery_Btn(object sender, RoutedEventArgs e)
        {
            var test = Newspaper_ListView.Displaying_ListView.SelectedItem;
        }
    }
}
