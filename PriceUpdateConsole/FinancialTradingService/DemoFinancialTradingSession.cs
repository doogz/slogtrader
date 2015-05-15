using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialTradingService.Instrument;
using FinancialTradingService.Model;

namespace FinancialTradingService
{
    class DemoFinancialTradingSession : IFinancialTradingSession
    {
        private readonly Dictionary<string, List<IPriceUpdateReceiver>> _priceUpdateSubscribers = new Dictionary<string, List<IPriceUpdateReceiver>>();

        /// <summary>
        /// Registers clients for callback notifications when price updates occur for the selected instrument.
        /// </summary>
        /// 
        /// <param name="instrument">
        /// Instrument symbol e.g. MSFT for Microsoft Corp stock
        /// </param>
        /// <param name="priceUpdateReceiver">
        /// Call-back interface for price update notifications - clients need to implement the OnPriceUpdate method
        /// </param>
        /// <returns>bool indicating success</returns>
        public bool SubscribeToPriceUpdates(string instrument, IPriceUpdateReceiver priceUpdateReceiver)
        {
            if (string.IsNullOrEmpty(instrument))
                return false;

            var checkInst = InstrumentCatalogue.Find(instrument);
            if (checkInst == null)
                return false;

            // Do we already have a bunch of subscribers for this instrument?
            var instrumentSubscribers = _priceUpdateSubscribers[instrument];
            if (instrumentSubscribers != null)
            {
                instrumentSubscribers.Add(priceUpdateReceiver);
            }
            else
            {
                // ReSharper turns a two line new List();List.Add into a one liner using a 'collection initializer'.
                _priceUpdateSubscribers.Add(instrument, new List<IPriceUpdateReceiver> { priceUpdateReceiver });
            }
            return true;
        }

        /// <summary>
        /// Removes currently subscribed IPriceUpdateReceiver from list of subscribed receivers
        /// </summary>
        /// <param name="instrument"></param>
        /// <param name="priceUpdateReceiver"></param>
        /// <returns></returns>

        public bool UnsubscribeFromPriceUpdates(string instrument, IPriceUpdateReceiver priceUpdateReceiver)
        {
            if (string.IsNullOrEmpty(instrument)) return false;
            var instrumentSubscribers = _priceUpdateSubscribers[instrument];
            // TODO: Once ReSharper's had it's way, you do end up with pretty terse code:
            // Personally, I like that.
            return instrumentSubscribers != null && instrumentSubscribers.Remove(priceUpdateReceiver);

            // Note: This code is currently leaving an empty List<IPriceUpdateReceiver> in place
        }

        public IInstrumentDefinition[] GetInstruments(string filter)
        {
            return _stockInstruments;
            // TODO: Build some forex instruments in code (from simulated currencies), and append here
        }

        /// <summary>
        /// Bit weird 'TheInstruments'
        /// </summary>
        /// TODO: Resharper would have this private static readonly field as TheInstruments - ???
        private static readonly IInstrumentDefinition[] _stockInstruments =
        {
            new StockInstrument("MSFT", "Microsoft Corp."),
            new StockInstrument("AAPL", "Apple Inc."),
            new StockInstrument("GOOG", "Google Inc."),
            new StockInstrument("FB", "Facebook Inc."),
            new StockInstrument("INTC", "Intel Corporation")

        };

    }
}
