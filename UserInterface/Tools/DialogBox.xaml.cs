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
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour DialogBox.xaml
    /// </summary>
    public partial class DialogBox : Window
    {
		private bool Result { get; set; }

		private DialogBox(string question, string title)
		{
			InitializeComponent();
			this.Title = title;
			lblQuestion.Content = question;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			Result = true;
			this.DialogResult = true;
		}

		private void btnDialogKo_Click(object sender, RoutedEventArgs e)
		{
			Result = false;
			this.DialogResult = false;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			Application curApp = Application.Current;
			Window mainWindow = curApp.MainWindow;
			this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
			this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
			lblQuestion.Focus();
		}

		public static bool YesOrNoCancel(string question, string title)
		{
			DialogBox YesOrNo = new DialogBox(question, title);
			YesOrNo.ShowDialog();
			return YesOrNo.Result;
		}

	}
}
