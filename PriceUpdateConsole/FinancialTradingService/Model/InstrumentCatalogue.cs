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
    ///
    /// TODO: Review
    /// Containment of a collection class is preferred to inheriting from it. 
    /// Dictionary<> fits the bill here, we have a collection uniquely indexed by a symbolic code (Symbol)
    /// and frequent look-ups are anticipated. We note the repetition of Symbol field as Key and within Value.
    /// Alternatives - SortedList<K,V>, SortedDictionary<> ? Something without the repetition? Or do we not care, because it's just one
    /// additional reference (to the Symbol string)?

    /// </summary>
    public static class InstrumentCatalogue
    {
        // Read-only internal dictionary. Supports O(log n) lookup of Instrument
        private static readonly Dictionary<string, IInstrument> _catalogue=new Dictionary<string, IInstrument>();


        /// <summary>
        /// Adds a new instrument to the catalogue.
        /// If the instrument - uniquely identified by its Symbol property - already exists in the collection,
        /// an exception will be thrown.
        /// </summary>
        /// <param name="instrument">Reference to instrument implementing IInstrumentDefinition</param>
        /// <returns></returns>
        public static void Add(IInstrument instrument)
        {
            lock (_catalogue)
            {
                _catalogue.Add(instrument.Symbol, instrument); // Can throw
            }
        }

        public static void Remove(string instrumentSymbol)
        {
            lock (_catalogue)
            {
                _catalogue.Remove(instrumentSymbol);
            }
        }

        public static IInstrument[] ToArray()
        {
            IInstrument[] ret;
            lock (_catalogue)
            {
                ret = _catalogue.Values.ToArray();
            }
            return ret;
        }

        public static IInstrument Find(string symbol)
        {
            lock (_catalogue)
            {
                IInstrument ins;
                return _catalogue.TryGetValue(symbol, out ins) ? ins : null;
            }
        }

        /// <summary>
        /// Fast description look-up method.
        /// </summary>
        /// <param name="symbol">Identifies the instrument whose description is being looked up</param>
        /// <returns></returns>
        public static string GetDescription(string symbol)
        {
            lock (_catalogue)
            {
                IInstrument ins;
                return _catalogue.TryGetValue(symbol, out ins)
                    ? ins.Description
                    : string.Format("Unknown instrument {0}", symbol);
            }
        }

        public static int Count
        {
            get
            {
                lock (_catalogue)
                {
                    return _catalogue.Count;
                }
            }
        }

        public static string SymbolFromIndex(int idx)
        {
            return _catalogue.Keys.ElementAt(idx);
        }

    }
}
