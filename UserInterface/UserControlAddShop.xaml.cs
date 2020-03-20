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
        public UserControlAddShop()
        {
            InitializeComponent();
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
                bool shopCreated = DataFromGUI.AddShop(shopName, streetNumber, streetName, cityName);
                if (shopCreated)
                {
                    Reset();
                    //Show green message - shop creation successfull
                }
                else
                {
                    Reset();
                    //Show red message - shop creation failed due to invalid City
                }
            }
            else
            {
                //Show a yellow message telling to complete the form
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        
        private void Reset()
        {
            List<TextBox> textChildren = GetTextFieldsFromWrapPanels();
            foreach (TextBox tB in textChildren)
            {
                tB.Text = String.Empty;
            }
        }

        private bool CheckFormFilled()
        {
            bool isFilled = true;
            List<TextBox> textChildren = GetTextFieldsFromWrapPanels();
            foreach (TextBox tB in textChildren)
            {
                if (tB.Text == String.Empty)
                {
                    isFilled = false;
                }
            }
            return isFilled;
        }

        private List<TextBox> GetTextFieldsFromWrapPanels()
        {
            List<TextBox> textChildren = new List<TextBox>();
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
    }
}
