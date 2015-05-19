using FinancialTradingService.Model;

namespace FinancialTradingService.Instruments
{
    /// <summary>
    /// Forex instruments are constructed from the relative strengths of two currencies
    /// We can access these currencies via the IForexInstrument interface (when supported)
    /// </summary>
    internal interface IForexInstrument
    {
        string BaseCurrencySymbol { get; }
        string QuoteCurrencySymbol { get; }
            
    }

    class ForexInstrument: BaseInstrument, IForexInstrument
    {
        ForexInstrument(string symbol)
            :base(symbol, Market.Forex)
        {
        }
        /// <summary>
        /// IForexInstrument interface implementation:
        /// </summary>
        public string BaseCurrencySymbol
        {
            get { return Symbol.Substring(0, 3); }
        }

        public string QuoteCurrencySymbol
        {
            get { return Symbol.Substring(3, 3); }
        }


        /// <summary>
        /// A description for a Forex instrument can be generated from the two currency codes
        /// comprising the instrument's symbol.
        /// </summary>
        public override string Description
        {
            // A forex instrument's description is defined by its two currencies, and a static method exists to get to this easily.
            get
            {
                return string.Format("{0} / {1}", Currency.GetDescription(BaseCurrencySymbol),
                    Currency.GetDescription(QuoteCurrencySymbol));
            }

        }

        
    }

  
}
