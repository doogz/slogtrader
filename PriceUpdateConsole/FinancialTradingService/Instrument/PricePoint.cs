using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService.Instrument
{
    /// <summary>
    /// TODO: Review please
    /// The nugget of information relating to a single price point in time is at least a candidate for a ValueType (struct).
    /// We're going to have a lot of them, so performance differences between value type and reference type could make a big overall difference.
    /// They also map directly to a 'point' on a graph (or the two points representing bid and ask respectively, at a time t, if you prefer)
    /// NOW THEN.
    /// It is certainly way bigger than the recommended 16 byte max for structs
    ///  - each decimal is 16 bytes on its own, plus 8 for timestamp, that's 40 per struct. 
    /// I'm guessing the pain is with copying value types repeatedly.
    /// 
    /// Having chosen to try it as a struct, I also:
    /// (1) DO NOT provide a default constructor for a struct.
    /// (2) DO NOT enable public mutators - all property 'set's are private
    /// (3) Ensure all-zeros default layout is 'valid'
    /// (4) Implement IEquatable<T>
    /// 
    /// Furthermore, I'll be be keeping my eye out on where and how often this type is exposed, as we won't want lots of value copying.
    /// 
    /// My understanding is that the .NET compiler will create a special implementation for value-type collections such as List<T>,
    /// preventing the need for the boxing that would occur eg using ArrayList.
    /// As we'll be constantly iterating over historic price points during the drawing of charts, and the calculation of analyses, 
    /// these memory and performance differences could begin to add up as we load the system, and as it gathers historic data.
    /// We can recognise that now, hence I've thought this struct implementation made sense.
    /// I'd be interested in testing the differences over various data sets, and see how things change as the size of struct varies
    /// TODO: Maybe try some performance testing (not ever done that yet either)
    /// </summary>
    public struct PricePoint : IEquatable<PricePoint>
    {
        public PricePoint(decimal bid, decimal ask, DateTime timestamp)
            : this()
        {
            AskPrice = ask;
            BidPrice = bid;
            Timestamp = timestamp;
        }
        
        /// <summary>
        /// Currency-precision 'ask price' (price to Buy)
        /// </summary>
        public decimal AskPrice { get; private set; }

        /// <summary>
        /// Currency-precision 'bid price' (price to Sell)
        /// </summary>
        public decimal BidPrice { get; private set; }

        /// <summary>
        /// UTC timestamp indicating time point at which the prices were issued
        /// </summary>
        public DateTime Timestamp { get; private set; }

        public bool Equals(PricePoint other)
        {
            return AskPrice == other.AskPrice && BidPrice == other.BidPrice && Timestamp == other.Timestamp;
        }
    }
}
