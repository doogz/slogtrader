namespace FinancialTradingService.Instrument
{
    /// <summary>
    /// Instrument definition returned by the service is, by its nature, read-only.
    /// An interface constituted of read-only properties is fit for purpose.
    /// </summary>
    public interface IInstrumentDefinition
    {
        string Symbol { get; } // eg MSFT, USDGBP etc
        string Description { get; } // eg Microsoft Corp., US Dollar / British Pound Sterling
        Market TradingMarket { get; } // eg Forex, Stock, CFDs
        string BaseCurrency { get; }    // eg USD, GBP
    }
}

/*
        void UpdatePricing(decimal bidPrice, decimal askPrice);
        void ResetPriceHistory();
        
        decimal CurrentBidPrice { get; }
        decimal CurrentAskPrice { get; }
        decimal CurrentSpread { get; }
        
        // Price history properties
        decimal HighBidPrice { get; }
        decimal LowBidPrice { get; }
        
        decimal HighAskPrice { get; }
        decimal LowAskPrice { get; }
    }
}
 */
