using System.Windows.Controls;

namespace ICE.StockMonitor.UI.Component.StockPrice
{
    public partial class StockPriceWatcher : UserControl
    {
        public StockPriceWatcher()
        {
            InitializeComponent();
        }

        private StockPriceWatcherViewModel ViewModel
        {
            get => DataContext as StockPriceWatcherViewModel;
            set => DataContext = value;
        }
    }
}