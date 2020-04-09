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
using Project_3___Press_Project;
using System.Linq;
using System.ComponentModel;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlCatalog.xaml
    /// </summary>
    public partial class UserControlCatalog : UserControl
    {

        private string _currentAction;

        public UserControlCatalog()
        {
            InitializeComponent();
            using(var context = new PressContext())
            {
                List<Newspaper> newspapers = (from n in context.Newspapers.ToList()
                                              join c in context.Catalogs.ToList()
                                              on n.NewspaperId equals c.Newspaper.NewspaperId
                                              select n).ToList();
    
                neswpaperselector_comboBox.ItemsSource = newspapers.OrderBy(n => n.Name);
            }
        }

        private void neswpaperselector_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListViewCatalog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(neswpaperselector_comboBox.SelectedItem != null)
            { 
                _currentAction = "Add";
                GetInfoCatalog.Visibility = Visibility.Visible;
                FreezeWindow();
            }
            else
            {
                DialogBox.OK("Please select a newspaper to add an edition.", "Newspaper not selected");
            }
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (catalog_listview.SelectedItem != null)
            {
                _currentAction = "Update";
                Catalog catalog = (Catalog)catalog_listview.SelectedItem;
                name_TextBox.Text = catalog.Name;
                PublicationDate_DatePicker.DisplayDate = catalog.PublicationDate;
                FreezeWindow();
                GetInfoCatalog.Visibility = Visibility.Visible;
            }
            else
            {
                DialogBox.OK("Please select a catalog to update.", "Catalog selection missing");
            }
        }

        private void FreezeWindow()
        {
            AddButton.IsEnabled = false;
            ModifyButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            neswpaperselector_comboBox.IsEnabled = false;
            catalog_listview.IsEnabled = false;
        }

        private void UnFreezeWindow()
        {
            AddButton.IsEnabled = true;
            ModifyButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            neswpaperselector_comboBox.IsEnabled = true;
            catalog_listview.IsEnabled = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(catalog_listview.SelectedItem != null)
            {
                bool confirmDelete = DialogBox.YesOrNoCancel("Confirm deletion ?", "Delete confirmation");
                if(confirmDelete)
                {
                    Catalog catalog = (Catalog)catalog_listview.SelectedItem;
                    catalog.DeleteInDB();
                    RefreshListViewCatalog();
                }
            }
            else
            {
                DialogBox.OK("Please select a catalog to delete.", "Catalog selection missing");
            }
        }

        private void RefreshListViewCatalog()
        {
            Newspaper newspaper = (Newspaper)neswpaperselector_comboBox.SelectedItem;
            List<Catalog> catalogs;
            using (var context = new PressContext())
            {
                catalogs = context.Catalogs.Where(c => c.Newspaper.NewspaperId == newspaper.NewspaperId).ToList();
            }
            catalog_listview.ItemsSource = catalogs;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string name = name_TextBox.Text;
            Newspaper newspaper = (Newspaper)neswpaperselector_comboBox.SelectedItem;
            DateTime PublicationDate = PublicationDate_DatePicker.SelectedDate.Value;
            Catalog catalog;
            if (_currentAction == "Add")
            {
                catalog = CatalogFactory.Create(name, PublicationDate, newspaper);
            }
            else
            {
                catalog = (Catalog) catalog_listview.SelectedItem;
                catalog.PublicationDate = PublicationDate;
                catalog.Name = name;
            }
            catalog.SaveInDB();
            RefreshListViewCatalog();
            ResetForm();
        }

        public void ResetForm()
        {
            GetInfoCatalog.Visibility = Visibility.Collapsed;
            name_TextBox.Text = "";
            PublicationDate_DatePicker.DisplayDate = DateTime.Now;
            UnFreezeWindow();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GetInfoCatalog.Visibility = Visibility.Collapsed;
            ResetForm();
        }
    }
}
