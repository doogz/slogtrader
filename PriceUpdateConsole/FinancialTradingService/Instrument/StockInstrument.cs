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
        public StockInstrument(string symbol, string description) : base(symbol, Market.Stock)
        {
            _description = description;
        }
    }
}
