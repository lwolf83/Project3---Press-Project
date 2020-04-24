using System;
using System.Windows;

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

		public static bool OK(string question, string title)
		{
			DialogBox OK = new DialogBox(question, title);
			OK.btnDialogCancel.Visibility = Visibility.Collapsed;
			OK.btnDialogKo.Visibility = Visibility.Collapsed;
			OK.btnDialogOk.HorizontalAlignment = HorizontalAlignment.Center;
			OK.ShowDialog();
			return OK.Result;
		}

	}
}
