using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICE.StockMonitor.Core.Service
{
    public delegate void StockPriceChangeEvent(object sender, StockPriceChangeEventArgs args);

    public class StockPriceChangeEventArgs
    {
        public string Symbol { get; set; }
        public double Price { get; set; }

        public StockPriceChangeEventArgs(string symbol, double price)
        {
            Symbol = symbol;
            Price = price;
        }
    }
}
