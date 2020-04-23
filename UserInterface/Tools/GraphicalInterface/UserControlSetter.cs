using System;
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
                case "MainMenu":
                    resControl = new UserControlMainMenu();
                    break;
                case "ItemAccount":
                    resControl = new UserControlAccount();
                    break;
                case "ItemAddNewspapers":
                    resControl = new UserControlAddNewspaper();
                    break;
                case "ItemCatalog":
                    resControl = new UserControlCatalog();
                    break;
                case "ItemClose":
                    resControl = new UserControlClose();
                    break;
                case "ItemNewspaper":
                    resControl = new UserControlJournal();
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
                case "ItemStock":
                    resControl = new UserControlStock();
                    break;
                case "AddShop":
                    resControl = new UserControlAddShop();
                    break;
                case "ItemEditors":
                    resControl = new UserControlEditor();
                    break;
                case "ItemAddEditors":
                    resControl = new UserControlAddEditor();
                    break;
                case "ItemModifyEditors":
                    resControl = new UserControlModifyEditor();
                    break;
                case "ItemEditions":
                    resControl = new UserControlEdition();
                    break;
                case "ItemAutomaticDelivery":
                    resControl = new UserControlAutomaticDelivery();
                    break;
                case "ItemModifyNewspaper":
                    resControl = new UserControlModifyNewspaper();
                    break;
                case "ItemModifyShop":
                    resControl = new UserControlModifyShop();
                    break;
                default:
                    resControl = new UserControlShop();
                    break;
            }      

            return resControl;
        }

    }
}
