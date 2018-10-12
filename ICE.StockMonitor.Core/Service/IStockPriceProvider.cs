using System.Collections.Generic;

namespace ICE.StockMonitor.Core.Service
{
    

    public interface IStockPriceProvider
    {
        void StartEngine();
        void StopEngine();

        event StockPriceChangeEvent OnStockPriceChangeEvent;
    }
}