﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;
using System.Linq;


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
            IEnumerable<Newspaper> newspapers = NewspaperReader.GetAll();
            neswpaperselector_comboBox.ItemsSource = newspapers.ToList().OrderBy(n => n.Name);
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
                PublicationDate_DatePicker.Text = catalog.PublicationDate.ToString("dd/MM/yyyy");
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
                FreezeWindow();
                bool confirmDelete = DialogBox.YesOrNoCancel("Confirm deletion ?", "Delete confirmation");
                if(confirmDelete)
                {
                    Catalog catalog = (Catalog)catalog_listview.SelectedItem;
                    catalog.DeleteInDB();
                    RefreshListViewCatalog();
                }
                UnFreezeWindow();
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

            if(PublicationDate_DatePicker.SelectedDate == null)
            {
                DialogBox.OK("Wrong date format", "Wrond date format");
                return;
            }
            DateTime publicationDate = PublicationDate_DatePicker.SelectedDate.Value;
            Catalog catalog;
            if (_currentAction == "Add")
            {
                catalog = CatalogFactory.Create(name, publicationDate, newspaper);
            }
            else
            {
                catalog = (Catalog) catalog_listview.SelectedItem;
                catalog.PublicationDate = publicationDate;
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
            PublicationDate_DatePicker.SelectedDate = null;
            UnFreezeWindow();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GetInfoCatalog.Visibility = Visibility.Collapsed;
            ResetForm();
        }
    }
}
