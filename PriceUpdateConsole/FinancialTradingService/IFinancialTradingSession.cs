using System;
using FinancialTradingService.Instrument;

namespace FinancialTradingService
{
    public interface IFinancialTradingSession
    {
        IInstrumentDefinition[] GetInstruments(string filter);

        bool SubscribeToPriceUpdates(string instrumentSymbol, IPriceUpdateReceiver priceUpdateReceiver);
        bool UnsubscribeFromPriceUpdates(string instrumentSymbol, IPriceUpdateReceiver priceUpdateReceiver);
    }
}
