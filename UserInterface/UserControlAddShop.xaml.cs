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
using System.Linq;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for UserControlAddShop.xaml
    /// </summary>
    public partial class UserControlAddShop : UserControl
    {
        IEnumerable<City> cities { get; set; }
        IEnumerable<Department> departments { get; set; }
        public List<string> cityNames { get; set; }
        
        public UserControlAddShop()
        {
            InitializeComponent();
            departments = ShopAdder_DB.GetDepartment();
            cities = ShopAdder_DB.GetCity();
            cityNames = cities.Select(x => x.Name).ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool formFilled = CheckFormFilled();
            if (formFilled)
            {
                string shopName = ShopName.Text;
                string streetNumber = StrNum.Text;
                string streetName = StrName.Text;
                string cityName = City.Text;
                bool shopCreated = ShopAdder_DB.AddShop(shopName, streetNumber, streetName, cityName);
                if (shopCreated)
                {
                    Reset();
                    MessageGrid.Background = Brushes.LightGreen;
                    Message.Content = "Shop is successfully created !";
                    MessageGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    Reset();
                    MessageGrid.Background = Brushes.IndianRed;
                    Message.Content = "Shop creation failed. Please enter a valid city";
                    MessageGrid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageGrid.Background = Brushes.LightGoldenrodYellow;
                Message.Content = "Please fill up all the required fields !";
                MessageGrid.Visibility = Visibility.Visible;
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        
        private void Reset()
        {
            List<TextBox> textChildren = GetTextFieldsFromChildren();
            foreach (TextBox tB in textChildren)
            {
                tB.Text = String.Empty;
            }
            Zip.Text = String.Empty;
            Dept.Text = String.Empty;
            City.Text = String.Empty;
        }

        private bool CheckFormFilled()
        {
            bool isFilled = true;
            List<TextBox> textChildren = GetTextFieldsFromChildren();
            foreach (TextBox tB in textChildren)
            {
                if (tB.Text == String.Empty)
                {
                    isFilled = false;
                }
            }
            if (City.Text == String.Empty)
            {
                isFilled = false;
            }
            return isFilled;
        }

        private List<TextBox> GetTextFieldsFromChildren()
        {
            List<TextBox> textChildren = new List<TextBox>();

            //foreach(UIElement child in parent.Children)
            //{
            //    if (child.GetType() == typeof(TextBox))
            //    { textChildren.Add((TextBox)child); }
            //    else if (child.GetType() == typeof(Panel))
            //    { GetTextFieldsFromChildren((Panel)child); }
            //}

            var wrapChildren = from child in TextStack.Children.OfType<WrapPanel>()
                               select child;

            foreach (WrapPanel child in wrapChildren)
            {
                var textBoxes = (from tB in child.Children.OfType<TextBox>()
                                 select tB).ToList();
                textChildren.AddRange(textBoxes);
            }
            return textChildren;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageGrid.Visibility = Visibility.Hidden;
        }

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
            }

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
                Zip.Text = cities.Where(c => c.Name.Equals(City.Text)).Select(c => c.ZipCode).FirstOrDefault();
                Dept.Text = departments.Where(d => d.DepartmentCode.Equals(deptCode)).Select(d => d.DepartmentName).FirstOrDefault();
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
    }
}
