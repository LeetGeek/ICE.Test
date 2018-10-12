using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICE.StockMonitor.Core.Service;

namespace ICE.StockMonitor.Core.Emulator
{
    public static class StockPriceEmulatorUtil
    {
        private static readonly Random _randGenerator = new Random();
        private const double TickSize = 0.01;

        public static bool UpdatePrice(ref StockPrice stock)
        {
            if (_randGenerator.Next(0, 2) != 1)
                return false;

            var walkUp = Convert.ToBoolean(_randGenerator.Next(0, 2));
            var tickToUse = walkUp ? TickSize : -1 * TickSize;

            stock.Price += tickToUse;

            return true;
        }
    }
}
