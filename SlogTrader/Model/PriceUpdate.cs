using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlogTrader.Model
{
    class PriceUpdate
    {
        public string InstrumentCode { get; private set; }
        public decimal AskPrice { get; private set; }
        public decimal BidPrice { get; private set; }
    }
}
