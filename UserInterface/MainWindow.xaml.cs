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
                
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemLogout");
        }

        private void ButtonCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemClose");                     
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

        private void ButtonSeeEditions_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemEditions");

        private void ButtonSeeCatalog_Click(object sender, RoutedEventArgs e)
        {
            UserControlSetter.SetGridMain(GridMain, "ItemCatalog");

        }
    }
}
