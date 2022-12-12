using System.Diagnostics; // Für "Debug" notwendig!

namespace AAIC.API.CLient.Sage100.Mobile.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.FirstChanceException += (sender, e) =>
            Debug.WriteLine("FirstChanceException: " + e.Exception.ToString());

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            Debug.WriteLine("UnhandledException: " + e.ExceptionObject.ToString());

            MainPage = new AppShell();
        }
    }
}