﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace UserInterface
{
    /// <summary>
    /// Set the Grid MainGrid 
    /// </summary>
    public class UserControlSetter
    {
        public static void SetGridMain(UserControl origin, String name)
        {
            Grid GridParent = (origin.Parent as Grid);
            SetGridMain(GridParent, name);
        }

        public static void SetGridMain(Grid grid, String name)
        {
            UserControl destination = GetUserControlByName(name);
            if (grid.Name == "GridMain")
            {
                grid.Children.Clear();
                grid.Children.Add(destination);
            }
        }

        private static UserControl GetUserControlByName(string name)
        {
            UserControl resControl;
            switch (name)
            {
                case "ItemAccount":
                    resControl = new UserControlAccount();
                    break;
                case "ItemClose":
                    resControl = new UserControlClose();
                    break;
                case "ItemEditor":
                    resControl = new UserControlEditor();
                    break;
                case "ItemLogin":
                    resControl = new UserControlLogin();
                    break;
                case "ItemLogout":
                    resControl = new UserControlLogout();
                    break;
                case "ItemOrder":
                    resControl = new UserControlOrder();
                    break;
                case "ItemSetting":
                    resControl = new UserControlSetting();
                    break;
                case "ItemShop":
                    resControl = new UserControlShop();
                    break;
                case "ItemStat":
                    resControl = new UserControlStat();
                    break;
                default:
                    resControl = new UserControlShop();
                    break;
            }

            return resControl;
        }

    }
}
