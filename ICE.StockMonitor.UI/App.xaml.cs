using System.IO;
using System.Reflection;
using System.Windows;

namespace ICE.StockMonitor.UI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper bs;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            bs = new Bootstrapper();
            bs.Run();
        }
    }
}