using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService
{
    public class PriceUpdate : IPriceUpdate
    {
        public PriceUpdate(string symbol, decimal bidPrice, decimal askPrice, DateTime serverTimestamp)
        {
            Instrument = symbol;
            BidPrice = bidPrice;
            AskPrice = askPrice;
            ServerTimestamp = serverTimestamp;
        }

        public string Instrument
        {
            get;set;
        }

        public decimal BidPrice
        {
            get;set;
        }

        public decimal AskPrice
        {
            get;set;
        }

        public DateTime ServerTimestamp
        {
            get;set;
        }
    }
}
