using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ICE.StockMonitor.Core.Emulator;
using ICE.StockMonitor.Core.Service;

namespace ICE.StockMonitor.PriceService1
{
    [Export(typeof(IStockPriceProvider))]
    public class StockPriceProvider1 : IStockPriceProvider
    {
        private readonly Random _randGenerator;
        private readonly Timer _timer;
        private readonly StockPrice[] _stockPrices;

        public StockPriceProvider1()
        {
            _stockPrices = new StockPrice[3]
            {
                new StockPrice()
                {
                    Symbol = "XXX",
                    Price = 11.11
                },
                new StockPrice()
                {
                    Symbol = "YYY",
                    Price = 22.22
                },
                new StockPrice()
                {
                    Symbol = "ZZZ",
                    Price = 33.33
                },
            };

            _timer = new Timer(50);
            _timer.Elapsed += _timer_Elapsed;
        }



        public void StartEngine()
        {
            _timer.Start();
        }

        public void StopEngine()
        {
            _timer.Stop();
        }

        public event StockPriceChangeEvent OnStockPriceChangeEvent;







        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < _stockPrices.Length; i++)
            {
                var stock = _stockPrices[i];
                if (StockPriceEmulatorUtil.UpdatePrice(ref stock))
                {
                    OnStockPriceChangeEvent?.Invoke(this, new StockPriceChangeEventArgs(stock.Symbol, stock.Price));
                }
            }
        }
    }
}

