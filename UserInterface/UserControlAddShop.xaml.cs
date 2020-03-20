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

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {

            List<TextBox> textChildren = new List<TextBox>();
            var wrapChildren = from child in TextStack.Children.OfType<WrapPanel>()
                               select child;

            foreach (WrapPanel child in wrapChildren)
            {
                var textBoxes = (from tB in child.Children.OfType<TextBox>()
                                 select tB).ToList();
                textBoxes[0].Text = String.Empty;
            }
        }
    }
}
