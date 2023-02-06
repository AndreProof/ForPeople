using System.Windows;
using ForPeople.Services;

namespace ForPeople
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Приложение.
        /// </summary>
        public App() 
            : base()
        {
            WindowService.MainWindow = this.MainWindow;
        }
    }
}
