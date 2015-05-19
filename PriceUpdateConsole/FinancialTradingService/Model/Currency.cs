using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Model
{
    /// <summary>
    /// The demo trading service generates believable Forex pricing by modeling changes in the relevant strengths of foreign
    /// currencies versus the US dollar.
    /// This means we can keep Forex reciprocals (e.g USDGBP and GBPUSD) meaningfully linked.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="description"></param>
        /// <param name="displaySymbol"></param>
        /// <param name="dollarRate"></param>
        public Currency(string currencyCode, string description, string displaySymbol, decimal dollarRate)
        {
            Code = currencyCode;
            Description = description;
            DisplaySymbol = displaySymbol;
        }


        /// <summary>
        /// Each currency is identified by a code (these are standardised in ISO 4217)
        /// </summary>
        public string Code { get; private set; }
        
        public string Description { get; private set; }
        public string DisplaySymbol { get; private set; }

        /// <summary>
        /// Define the top currencies.
        /// Each is defined relative to the dollar - the 'dollar rate' is the amount of the currency equal to 1USD$
        /// These are only seen in this form internally and are read only too.
        /// </summary>
        private static readonly Currency[] Majors =
        {
            new Currency("USD", "U.S. Dollar", "$", 1.0m),
            new Currency("EUR", "Euro", "€", 0.88m),
            new Currency("JPY", "Japanese Yen", "¥", 119.41m),
            new Currency("GBP", "British Pound Sterling", "£", 0.66m ),
            new Currency("CHF", "Swiss Franc", "Fr.", 0.91m),
            new Currency("CAD", "Canadian Dollar", "$", 1.21m),
            new Currency("AUD", "Australian Dollar", "$", 1.26m),
            new Currency("NZD", "New Zealand Dollar", "$", 1.34m),
            new Currency("ZAR", "South African Rand", "R", 12.09m ),
        };

        /// <summary>
        /// Static method for providing a description of a currency from its ISO currency code.
        /// </summary>
        /// <param name="currencyCode">The ISO currency code. It is 3 upper-case characters e.g. USD, GBP</param>
        /// <returns></returns>
        public static string GetDescription(string currencyCode)
        {
            if (currencyCode == null)
            {
                throw new ArgumentNullException("currencyCode");
            }

            var searchCode = currencyCode.ToUpper();
            
            // Using FirstOrDefault Enumerable extension method is quite neat:
            var foundCurrency = Majors.FirstOrDefault(c => c.Code == searchCode);
            return foundCurrency == null
                ? string.Format("Unknown currency code {0}", currencyCode)
                : foundCurrency.Description;

            
            // Here's what I wrote originally
            /*
            foreach (var c in Majors)
                if (c.Code == currencyCode)
                    return c.Description;
            */

            // ReSharper then recommends this conversion:
            /*
            foreach (var c in Majors.Where(c => c.Code == currencyCode))
                return c.Description;
             */

            /*
             * TODO: So, that buys us 1 less line of code - but invokes completely new Linq processing. There will be a performance penalty for the creation of new iterator objects, plus another during their lazy evaluation later. 
             * SO: Is the ReSharper suggestion overly keen?
             * I can hear cries of 'premature optimisation', worrying about the performance of Linq.
             * However, I'd make the point - ReSharper has rewritten my code, and added more than just a little overhead.
             * Compare returning a value immediately to creating a new iterator object, using lazy evaluation on it, and disposing of it is ALL
             * hard work being done on our behalf, and I don't think it's trivial.
             * If the only reason for ReSharper's suggestion is 'Because it reduces the number of lines of code by 1' - well. Not convinced.
             */

        }

    }
}
