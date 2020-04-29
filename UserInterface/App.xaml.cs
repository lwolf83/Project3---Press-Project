using System.Windows;
using Project_3___Press_Project;

namespace UserInterface
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var context = new PressContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
