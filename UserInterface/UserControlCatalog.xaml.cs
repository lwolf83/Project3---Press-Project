using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_3___Press_Project;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Factory;
using System.Linq;
using Project_3___Press_Project.Repository;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour UserControlCatalog.xaml
    /// </summary>
    public partial class UserControlCatalog : UserControl
    {
        private readonly CatalogRepository _catalogRepository;
        private string _currentAction;

        public UserControlCatalog()
        {
            InitializeComponent();
            IEnumerable<Newspaper> newspapers = NewspapersLoader.GetAll();
            neswpaperselector_comboBox.ItemsSource = newspapers.AsEnumerable().OrderBy(n => n.Name);
        }

        private void neswpaperselector_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListViewCatalog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (neswpaperselector_comboBox.SelectedItem == null)
            {
                DialogBox.OK("Please select a newspaper to add an edition.", "Newspaper not selected");
                return;
            }
            _currentAction = "Add";
            GetInfoCatalog.Visibility = Visibility.Visible;
            FreezeWindow();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (catalog_listview.SelectedItem == null)
            {
                DialogBox.OK("Please select a catalog to update.", "Catalog selection missing");
                return;
            }
            _currentAction = "Update";
            Catalog catalog = (Catalog)catalog_listview.SelectedItem;
            name_TextBox.Text = catalog.Name;
            PublicationDate_DatePicker.DisplayDate = catalog.PublicationDate;
            PublicationDate_DatePicker.Text = catalog.PublicationDate.ToString("dd/MM/yyyy");
            FreezeWindow();
            GetInfoCatalog.Visibility = Visibility.Visible;
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
            if (catalog_listview.SelectedItem == null)
            {
                DialogBox.OK("Please select a catalog to delete.", "Catalog selection missing");
                return;
            }
            FreezeWindow();
            bool confirmDelete = DialogBox.YesOrNoCancel("Confirm deletion ?", "Delete confirmation");
            if(confirmDelete)
            {
                var catalog = catalog_listview.SelectedItem as Catalog;
                _catalogRepository.Delete(catalog);
                RefreshListViewCatalog();
            }
            UnFreezeWindow();
 
        }

        private void RefreshListViewCatalog()
        {
            Newspaper newspaper = (Newspaper)neswpaperselector_comboBox.SelectedItem;
            IEnumerable<Catalog> catalogs = CatalogLoader.Get(newspaper);
            catalog_listview.ItemsSource = catalogs.AsEnumerable().OrderByDescending(c => c.PublicationDate);
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
            _catalogRepository.Update(catalog);
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
