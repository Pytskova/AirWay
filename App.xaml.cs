using System.Windows;
using AirlineApp.Data;

namespace AirlineApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DbInitializer.Initialize();


        }
    }
}
