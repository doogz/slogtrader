using FinancialTradingService.Model;

namespace FinancialTradingService.Instruments
{
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
