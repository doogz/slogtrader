using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SlogTrader.Model
{
    class Currency
    {
        public Currency(string currencyCode, string description, string symbol, decimal dollarRate)
        {
            Code = currencyCode;
            Description = description;
            Symbol = symbol;
        }
        /// <summary>
        /// Each currency is identified by a code (these are standardised in ISO 4217)
        /// </summary>
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string Symbol { get; private set; }

        /// <summary>
        /// Define the top currencies.
        /// Each is defined relative to the dollar - the 'dollar rate' is the amount of the currency equal to 1USD$
        /// </summary>
        public static Currency[] Majors =
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

        public static string GetDescription(string currencyCode)
        {
            if (currencyCode == null)
            {
                throw new System.ArgumentNullException("currencyCode");
            }

            var searchCode = currencyCode.ToUpper();
            
        
            foreach(var c in Majors)
                if (c.Code == currencyCode)
                    return c.Description;

            var c2 = Majors.FirstOrDefault(c => c.Code == currencyCode);
            return c2 != null ? c2.Description : "Unknown";
        }



    }
}
