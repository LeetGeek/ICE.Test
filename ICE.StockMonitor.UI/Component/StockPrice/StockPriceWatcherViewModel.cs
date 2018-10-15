using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using ICE.StockMonitor.Core.Service;
using Prism.Commands;
using Prism.Mvvm;

namespace ICE.StockMonitor.UI.Component.StockPrice
{
    [Export]
    internal class StockPriceWatcherViewModel : BindableBase, IPartImportsSatisfiedNotification
    {
        private ICommand _startEngineCommand;


        private List<StockPriceItem> _stockPriceItems;

        private ICommand _stopEngineCommand;

        [Import]
        private StockPriceProvider client;


        public StockPriceWatcherViewModel()
        {
            _stockPriceItems = new List<StockPriceItem>();
        }

        public ICommand StartEngineCommand =>
            _startEngineCommand ?? (_startEngineCommand = new DelegateCommand(StartEngine));

        public List<StockPriceItem> StockPriceItems
        {
            get => _stockPriceItems;
            set => SetProperty(ref _stockPriceItems, value);
        }

        public ICommand StopEngineCommand =>
            _stopEngineCommand ?? (_stopEngineCommand = new DelegateCommand(StopEngine));


        public void OnImportsSatisfied()
        {
            client.OnStockPriceChangeEvent += Client_OnStockPriceChangeEvent;
        }

        private void StartEngine()
        {
            client.StartEngine();
        }

        private void StopEngine()
        {
            client.StopEngine();
        }

        private void Client_OnStockPriceChangeEvent(object sender, StockPriceChangeEventArgs args)
        {
            var list = StockPriceItems.ToList();
            var item = list.FirstOrDefault(stock => stock.Symbol == args.Symbol);

            if (item == null)
            {
                list.Add(new StockPriceItem(args.Symbol, args.Price));
                list.Sort((item1, item2) => item1.Symbol.CompareTo(item2.Symbol));
                StockPriceItems = list;
            }
            else
            {
                item.Price = args.Price;
            }

            StockPriceItems = list;
        }
    }
}