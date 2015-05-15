using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService
{
    // A price update refers to one instrument only and contains both a buying price and a selling price.
    // Each price update is timestamped by the server, and refers to the time at which the price applies.
    // Prices could change before the price update is received by clients.
    public interface IPriceUpdate
    {
        string Instrument { get; }
        decimal BidPrice { get; }   // Price for selling
        decimal AskPrice { get; }   // Price to buy
        DateTime ServerTimestamp { get; }
    }
}
