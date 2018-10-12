using Prism.Mvvm;

namespace ICE.StockMonitor.UI.Component.StockPrice
{
    internal class StockPriceItem : BindableBase
    {
        private double _price;

        public StockPriceItem(string symbol, double price)
        {
            Symbol = symbol;
            Price = price;
        }

        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public string Symbol { get; set; }
    }
}