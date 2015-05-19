using System;

namespace FinancialTradingService.Instrument
{
    /// <summary>
    /// Instrument definition returned by the service.
    /// </summary>
    public interface IInstrument
    {
        string Symbol { get; } // eg MSFT, USDGBP etc
        string Description { get; } // eg Microsoft Corp., US Dollar / British Pound Sterling
        Market TradingMarket { get; } // eg Forex, Stock, CFDs
        string BaseCurrency { get; }    // eg USD, GBP
        decimal CurrentBidPrice { get; }
        decimal CurrentAskPrice { get; }
        decimal CurrentSpread { get; }
        void UpdatePricing(decimal bidPrice, decimal askPrice, DateTime timestamp);
        void SubscribeToPriceUpdates(IPriceUpdateReceiver updateReceiver);
        void UnsubscribeFromPriceUpdates(IPriceUpdateReceiver updateReceiver);
    }
}