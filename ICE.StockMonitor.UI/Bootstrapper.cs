using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using ICE.StockMonitor.Core.Service;
using Prism.Mef;

namespace ICE.StockMonitor.UI
{
    internal class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StockPrice).Assembly));
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PriceServices")));


            base.ConfigureAggregateCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (MainWindow) Shell;
            Application.Current.MainWindow.Show();
        }
    }
}