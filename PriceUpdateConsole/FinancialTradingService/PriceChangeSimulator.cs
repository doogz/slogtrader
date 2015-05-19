using System;
using System.Diagnostics;
using System.Threading;
using FinancialTradingService.Instrument;
using FinancialTradingService.Model;

namespace FinancialTradingService
{
    static class PriceChangeSimulator
    {
        private static readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        private static bool _keepGoing = true;

        /// <summary>
        /// I'm aware that by default, new threads are created as foreground threads, and that this thread _could_
        /// have been set to be a background thread (in which case, it'd be stopped as soon as the main thread stopped.)
        /// I've intentionally left it as a foreground thread, to force me to programatically shut it down.
        /// 
        /// </summary>
        public static void Run()
        {
            // This thread randomly changes instrument prices over time.
            // Each time there is a change, we need to notify attached clients too.
            var rand = new Random();
            do
            {
                // Random delay between 100ms and 500ms
                int delay = rand.Next(100, 500);
                Thread.Sleep(delay);

                // Followed by change of price of some instrument
                int instrumentIndex = rand.Next(0, InstrumentCatalogue.Count);
                string key = InstrumentCatalogue.SymbolFromIndex(instrumentIndex);
                
                // We'll do a change between 0.05% and 0.005%
                decimal delta = rand.Next(5, 50)/10000.0m;
                IInstrument inst = InstrumentCatalogue.Find(key);
                bool increase = rand.Next(1000) >= 500;
                decimal ask = increase ? inst.CurrentAskPrice*(1.0m + delta) : inst.CurrentAskPrice*(1.0m - delta);
                decimal bid = increase ? inst.CurrentBidPrice*(1.0m + delta) : inst.CurrentBidPrice*(1.0m - delta);
                Debug.WriteLine("Price update for {0}, Ask: {1}, Bid: {2}", key, ask, bid);
                inst.UpdatePricing(bid, ask, DateTime.Now);
                

            } while (_keepGoing);

            _waitHandle.Set();

        }

        public static void Stop()
        {
            _keepGoing = false;
            _waitHandle.WaitOne();


        }

    }
}
