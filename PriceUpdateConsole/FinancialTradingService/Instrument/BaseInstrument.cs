using System;
using System.Collections.Generic;
using FinancialTradingService.Instrument;
using FinancialTradingService.Model;

namespace FinancialTradingService
{
    public enum Market
    {
        Forex,
        CfDs,
        Stock
    }

    // Class marked public for consumption by subclasses elsewhere
    public abstract class BaseInstrument : IInstrument
    {
        private readonly Market _market;
        private readonly string _symbol;

        // For drawing charts, we'll need a history of price points:
        private readonly PriceHistory _history = new PriceHistory();

        // For notifying interested parties, we maintain a list of price update subscripbers, and service
        // them during the UpdatePricing method
        private readonly List<IPriceUpdateReceiver> _priceUpdateSubscribers = new List<IPriceUpdateReceiver>();


        protected BaseInstrument(string symbol, Market market) // Intended as base class only. 
        {
            _symbol = symbol;
            _market = market;
        }

        public string Symbol { get { return _symbol; } }

        public Market TradingMarket { get { return _market; } }

        public string BaseCurrency
        {
            get { return "USD"; }
        }

        /// <summary>
        /// BaseInstrument expects concrete implementations to provide a Description
        /// e.g. Forex uses the currency codes (embedded within the instrument code)
        /// </summary>
        public abstract string Description { get; }     // 

        private decimal _bidPrice;
        private decimal _askPrice;

        public void UpdatePricing(decimal bidPrice, decimal askPrice, DateTime timestamp)
        {
            // Add price point to history
            PricePoint p=new PricePoint(bidPrice, askPrice, timestamp);
            _history.AddPoint(ref p);

            // and update current values
            CurrentBidPrice = bidPrice;
            CurrentAskPrice = askPrice;

            // Finally, let any interested clients know
            var priceUpdate = new PriceUpdate(Symbol, bidPrice, askPrice, timestamp);
            NotifySubscribers(priceUpdate);
        }

        public void SubscribeToPriceUpdates(IPriceUpdateReceiver updateReceiver)
        {
            _priceUpdateSubscribers.Add(updateReceiver);
        }

        public void UnsubscribeFromPriceUpdates(IPriceUpdateReceiver updateReceiver)
        {
            _priceUpdateSubscribers.Remove(updateReceiver);
        }

        protected void NotifySubscribers(IPriceUpdate priceUpdate)
        {
            foreach (var c in _priceUpdateSubscribers)
                c.OnPriceUpdate(priceUpdate);
        }

        /// <summary>
        /// MarketName provides a default descriptive name for the Market field, using Enum.ToString()
        /// This provides reasonable behaviour for any new Markets we add.
        /// We'd want to override it for CFDs though - our code enumeration 'CfDs' matches default Resharper coding style,
        /// whereas we'd probably wish to show it on-screen as "CFD" or "CFDs".
        /// </summary>
        public virtual string MarketName
        {
            get { return _market.ToString(); }
        }
        // Updating the current bid price automatically updates the HighBidPrice and LowBidPrice
        public decimal CurrentBidPrice
        {
            get { return _bidPrice; }
            set
            {
                // New (or first ever) low price?
                if (LowBidPrice == 0.0m || value < LowBidPrice)
                    LowBidPrice = value;
                // New or first high price?
                if (HighBidPrice == 0.0m || value > HighBidPrice)
                    HighBidPrice = value;
                _bidPrice = value;
            }
        }

        // And updating the current ask price automatically updates the ask high/low
        public decimal CurrentAskPrice
        {
            get { return _askPrice; }
            set
            {
                if (LowAskPrice == 0.0m || value < LowAskPrice)
                    LowAskPrice = value;

                if (HighAskPrice == 0.0m || value > HighAskPrice)
                    HighAskPrice = value;

                _askPrice = value;
            }
        }

        public void ResetPriceHistory()
        {
            LowAskPrice = HighAskPrice = LowBidPrice = HighBidPrice = 0.0m;
        }

        public decimal CurrentSpread
        {
            get { return CurrentAskPrice - CurrentBidPrice; }
        }

        public decimal HighBidPrice { get; private set; }
        public decimal LowBidPrice { get; private set; }
        public decimal HighAskPrice { get; private set; }
        public decimal LowAskPrice { get; private set; }
    }
}
