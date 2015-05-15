using System;
using System.Collections.Generic;
using System.Linq;
using FinancialTradingService.Instrument;

namespace FinancialTradingService.Model
{
    /// <summary>
    /// The (static) instrument catalogue comprises the complete collection of financial instruments offered for sale via the trading service.
    /// General information (defined by the IInstrumentDefinition interface) is cached here in the catalogue. 
    ///
    /// Live pricing data is not available. Such information is collected by the Watchlist and WatchedInstrument classes.
    ///
    /// TODO: Review
    /// Containment of a collection class is preferred to inheriting from it. 
    /// Dictionary<> fits the bill here, we have a collection uniquely indexed by a symbolic code (Symbol)
    /// and frequent look-ups are anticipated.

    /// </summary>
    public static class InstrumentCatalogue
    {
        // Read-only internal dictionary
        private static readonly Dictionary<string, IInstrumentDefinition> _catalogue=new Dictionary<string, IInstrumentDefinition>();


        /// <summary>
        /// Adds a new instrument to the catalogue.
        /// If the instrument - uniquely identified by its Symbol property - already exists in the collection,
        /// an exception will be thrown.
        /// </summary>
        /// <param name="instrument">Reference to instrument implementing IInstrumentDefinition</param>
        /// <returns></returns>
        public static void Add(IInstrumentDefinition instrument)
        {
            _catalogue.Add(instrument.Symbol, instrument); // Can throw
        }

        public static void Remove(string instrumentSymbol)
        {
            _catalogue.Remove(instrumentSymbol);
        }

        public static IInstrumentDefinition Find(string symbol)
        {
            IInstrumentDefinition ins;
            return _catalogue.TryGetValue(symbol, out ins) ? ins : null;
        }
        /// <summary>
        /// Fast description look-up method.
        /// </summary>
        /// <param name="symbol">Identifies the instrument whose description is being looked up</param>
        /// <returns></returns>
        public static string GetDescription(string symbol)
        {
            IInstrumentDefinition ins;
            return _catalogue.TryGetValue(symbol, out ins) ? ins.Description : string.Format("Unknown instrument {0}", symbol);
        }

    }
}
