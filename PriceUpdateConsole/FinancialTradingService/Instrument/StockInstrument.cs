using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Instrument
{
    public class StockInstrument : BaseInstrument
    {
        public override string Description
        {
            get
            {
                return _description;
            }
        }

        private readonly string _description;
        public StockInstrument(string symbol, string description, decimal price) : base(symbol, Market.Stock)
        {
            _description = description;
            // Ask price is price to buy, bid price is price to sell, ask > bid
            CurrentAskPrice = price*1.01m;
            CurrentBidPrice = price*0.99m;
        }
    }
}
