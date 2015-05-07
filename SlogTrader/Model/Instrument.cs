using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlogTrader.Model
{
    /// <summary>
    /// Instrument models the static information pertaining to a financial instrument available for trading.
    /// Each instrument is of a particular Market e.g. Forex, Stock, CFD etc.
    /// and has a human-readable description as a well as a unique identifying code
    /// </summary>
    class Instrument
    {
        /// <summary>
        /// Constructors
        /// </summary>
        public Instrument(string id, string description, Market market, Currency baseCurrency)
        {
            Id=id;
            Description=description;
            TradingMarket=market;
        }
        
        public enum Market
        {
            Forex, Stock, CFD
        }
       
        public string Id { get; private set; }
        public Market TradingMarket { get; private set; }
        public string Description { get; set; }
        public decimal AskPrice { get; private set; }
        public decimal BidPrice { get; private set; }

        public Currency BaseCurrency
        {
            get
            {
                // Base currency
                string code = Id.Substring(0, 3);
                return new Currency
            }
        }

        public decimal Spread
        {
            get { return AskPrice - BidPrice; }
        }
    }
}
