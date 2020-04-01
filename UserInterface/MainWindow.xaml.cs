﻿using MaterialDesignThemes.Wpf;
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
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            UserControlSetter.SetGridMain(GridMain, "ItemLogin");
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
            UserControlSetter.SetGridMain(GridMain, "ItemLogout");
        }

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemClose");                     
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
    }
}
