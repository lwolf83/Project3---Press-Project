using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using Project_3___Press_Project;
using System.ComponentModel;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for CityAutocompletor.xaml
    /// </summary>
    public partial class CityAutocompletor : UserControl, INotifyPropertyChanged
    {
        public IEnumerable<City> cities { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<string> cityNames { get; set; }
        private string currDep = String.Empty;
        public string CurrDep
        {
            get
            {
                return currDep;
            }
            set
            {
                currDep = value;
                OnPropertyChanged("CurrDep");
            }
        }
        private string currZip = String.Empty;
        public string CurrZip
        {
            get
            {
                return currZip;
            }
            set
            {
                currZip = value;
                OnPropertyChanged("CurrZip");
            }
        }

        public CityAutocompletor()
        {
            InitializeComponent();
            departments = ShopAdder_DB.GetDepartment();
            cities = ShopAdder_DB.GetCity();
            cityNames = cities.Select(x => x.Name).ToList().Distinct();
        }

        protected 

        private void City_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            string query = City.Text;

            if (query.Length == 0)
            {
                resultStack.Children.Clear();
                Border.Visibility = Visibility.Collapsed;
            }
            else
            {
                Border.Visibility = Visibility.Visible;
                resultStack.Children.Clear();

                foreach (var city in cityNames)
                {
                    if (city.ToLower().StartsWith(query.ToLower()))
                    {
                        AddItem(city);
                        found = true;
                    }
                }
                if (!found)
                {
                    TextBlock block = new TextBlock();
                    block.Text = "No results found.";
                    block.Margin = new Thickness(2, 3, 2, 3);
                    block.Foreground = Brushes.Black;
                    resultStack.Children.Add(block);
                }
            }
        }

        private void AddItem(string text)
        {
            TextBlock block = new TextBlock();
            block.Text = text;

            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;
            block.Foreground = Brushes.Black;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                City.Text = (sender as TextBlock).Text;
                Border.Visibility = Visibility.Collapsed;
                string deptCode = cities.Where(c => c.Name.Equals(City.Text)).Select(c => c.DepartmentCode).FirstOrDefault();
                CurrZip = cities.Where(c => c.Name.Equals(City.Text)).Select(c => c.ZipCode).FirstOrDefault();
                CurrDep = departments.Where(d => d.DepartmentCode.Equals(deptCode)).Select(d => d.DepartmentName).FirstOrDefault();
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };
            resultStack.Children.Add(block);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border.Visibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
