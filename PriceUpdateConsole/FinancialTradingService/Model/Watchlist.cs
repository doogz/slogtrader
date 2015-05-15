
using System;
using System.Collections.Generic;
using FinancialTradingService.Instrument;

namespace FinancialTradingService.Model
{
    /// <summary>
    /// A watchlist is a collection of subscribed-to instruments, for which we are receiving price update information.
    /// </summary>
    public class Watchlist : IPriceUpdateReceiver
    {
        private readonly IFinancialTradingSession _session;
        public Watchlist(IFinancialTradingSession session)
        {
            _session = session;
        }

        private Dictionary<string, WatchedInstrument> _instruments;

        public bool StartWatchingInstrument(string symbol)
        {
            // Do we know about that instrument?
            IInstrumentDefinition instrument = InstrumentCatalogue.Find(symbol);
            if (instrument == null)
                return false;

            // Are we already watching it?
            if (_instruments[symbol] != null)
                return false;

            // Ok, it's new, and we want prices
            // Our Watchlist receives all the updates - it implements IPriceUpdateReceiver
            return _session.SubscribeToPriceUpdates(symbol, this);
        }

        public bool StopWatchingInstrument(string symbol)
        {
            throw new NotImplementedException("StopWatchingInstrument");
        }

        /// <summary>
        /// Call-back method, receiving price updates for all our watched instruments
        /// This is being driven by the Price Update Notification thread
        /// TODO: Review thread-safety
        /// </summary>
        /// <param name="update"></param>
        public void OnPriceUpdate(IPriceUpdate update)
        {
            string symbol = update.Instrument;

            // Due to network lag, it's possible we receive updates for instruments we've unsubscribed to.
            // If that's the case, we've got no more work to do.
            WatchedInstrument inst = _instruments[symbol];
            if (inst == null) return;

            //TODO: It might be more flexible to pass the price update object on, rather than pull out the data here
            inst.UpdatePrice(update.BidPrice, update.AskPrice, update.ServerTimestamp);
        }
    }
}
