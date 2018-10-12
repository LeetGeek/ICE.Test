using System.ComponentModel.Composition;
using System.Windows;
using ICE.StockMonitor.UI.Component.StockPrice;
using Microsoft.Practices.ServiceLocation;

namespace ICE.StockMonitor.UI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : Window, IPartImportsSatisfiedNotification
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            StockPriceWatcher.DataContext = ServiceLocator.Current.GetInstance<StockPriceWatcherViewModel>();
        }
    }
}