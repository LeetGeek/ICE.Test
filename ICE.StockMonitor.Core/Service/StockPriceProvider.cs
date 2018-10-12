using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICE.StockMonitor.Core.Service
{
    [Export]
    public class StockPriceProvider : IStockPriceProvider, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private IEnumerable<IStockPriceProvider> _stockPriceProviders;
        private readonly ConcurrentQueue<StockPriceChangeEventArgs> _stockPriceChanges;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isRunning = false;

        public event StockPriceChangeEvent OnStockPriceChangeEvent;


        public StockPriceProvider()
        {
            _stockPriceChanges = new ConcurrentQueue<StockPriceChangeEventArgs>();

        }




        public void StartEngine()
        {
            if (_isRunning) return;
            _isRunning = true;

            foreach (var stockPriceProvider in _stockPriceProviders)
            {
                stockPriceProvider.StartEngine();
            }

            _cts = new CancellationTokenSource();
            Task.Run(()=>StockPricesChangeDispatcher(_cts.Token), _cts.Token);
        }

        public void StopEngine()
        {
            if (!_isRunning) return;
            _isRunning = false;

            foreach (var stockPriceProvider in _stockPriceProviders)
            {
                stockPriceProvider.StopEngine();
            }

            _cts.Cancel();
        }


        public void OnImportsSatisfied()
        {
            foreach (var stockPriceProvider in _stockPriceProviders)
            {
                stockPriceProvider.OnStockPriceChangeEvent += StockPriceProvider_OnStockPriceChangeEvent;
            }
        }

        private void StockPriceProvider_OnStockPriceChangeEvent(object sender, StockPriceChangeEventArgs args)
        {
            _stockPriceChanges.Enqueue(args);
        }

        private void StockPricesChangeDispatcher(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_stockPriceChanges.TryDequeue(out var item))
                {
                    OnStockPriceChangeEvent?.Invoke(this, item);
                }
                Thread.Sleep(10);
            }
              
        }
    }
}
