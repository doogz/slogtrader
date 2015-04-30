using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlogTrader.Model
{
    /// <summary>
    /// Instrument models the static information pertaining to a financial instrument available for trading.
    /// Each instrument is of a particular type e.g. Forex, Stock, etc.
    /// and has a human-readable description as a well as a unique identifying code
    /// </summary>
    class Instrument
    {
        public enum Kind { Forex, Stock }
        public string Code { get; private set; }
        public Kind InstrumentType { get; private set; }
        public string Description { get; set; }

    }
}
