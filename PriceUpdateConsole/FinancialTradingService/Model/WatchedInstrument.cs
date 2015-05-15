using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialTradingService.Instrument;

namespace FinancialTradingService.Model
{
    //TODO: There's no history on the watched instrument yet

    public class WatchedInstrument
    {
        private decimal _bidPrice;
        private decimal _askPrice;
        private PriceHistory _history;

        public string Symbol { get; private set; }

        public void UpdatePrice(decimal bid, decimal ask, DateTime time)
        {
            _history.AddPricePoint(bid, ask, time);

            CurrentBidPrice = bid;
            CurrentAskPrice = ask;
        }

        // Updating the current bid price automatically updates the HighBidPrice and LowBidPrice
        public decimal CurrentBidPrice
        {
            get { return _bidPrice; }
            set
            {
                // New (or first ever) low price?
                if (LowBidPrice == 0.0m || value < LowBidPrice)
                    LowBidPrice = value;
                // New or first high price?
                if (HighBidPrice == 0.0m || value > HighBidPrice)
                    HighBidPrice = value;
                _bidPrice = value;
            }
        }

        // And updating the current ask price automatically updates the ask high/low
        public decimal CurrentAskPrice
        {
            get { return _askPrice; }
            set
            {
                if (LowAskPrice == 0.0m || value < LowAskPrice)
                    LowAskPrice = value;

                if (HighAskPrice == 0.0m || value > HighAskPrice)
                    HighAskPrice = value;

                _askPrice = value;
            }
        }

        public void ResetPriceHistory()
        {
            LowAskPrice = HighAskPrice = LowBidPrice = HighBidPrice = 0.0m;
        }

        public decimal CurrentSpread
        {
            get { return CurrentAskPrice - CurrentBidPrice; }
        }

        public decimal HighBidPrice { get; private set; }
        public decimal LowBidPrice { get; private set; }
        public decimal HighAskPrice { get; private set; }
        public decimal LowAskPrice { get; private set; }
    }

}
