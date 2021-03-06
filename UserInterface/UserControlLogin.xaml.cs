﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project_3___Press_Project;

namespace UserInterface
{
    public partial class UserControlLogin : UserControl
    {
        public UserControlLogin()
        {            
            InitializeComponent();
            Loaded += MyWpfControl_Load; 
        }

        private void MyWpfControl_Load(object sender, EventArgs e)
        {
            txtUserNameBox.Focus();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPasswordBox.Focus();
            }
        }

        private void txtPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserSingleton.GetInstance.Init(txtUserNameBox.Text, txtPasswordBox.Password);
            if (UserSingleton.GetInstance.IsAuthenticated)
            {
                MenuVisibility.ShowMenu();
                UserControlSetter.SetGridMain(this, "MainMenu");
            }
            else
            {
                txtMessageBox.Visibility = Visibility.Visible;
                Reset();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtUserNameBox.Text = String.Empty;
            txtPasswordBox.Password = String.Empty;
            txtUserNameBox.Focus();
        }


    }
}
