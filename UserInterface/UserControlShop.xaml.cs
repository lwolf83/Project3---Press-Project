﻿using System;
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
    public partial class UserControlShop : UserControl
    {
        public UserControlShop()
        {
            InitializeComponent();
        }

        private void ButtonAddShopPage_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = new UserControlAddShop();
            this.Content = usc;
        }
    }
}
