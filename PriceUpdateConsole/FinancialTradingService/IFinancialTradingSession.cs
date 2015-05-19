using System;
using FinancialTradingService.Instrument;

namespace FinancialTradingService
{
    public interface IFinancialTradingSession
    {
        /// <summary>
        /// Provides a snap-shot of availalable instruments
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IInstrument[] GetInstruments(string filter);

        /// <summary>
        /// Request to start receiving price updates for the given instrument.
        /// The client provides an IPriceUpdateReceiver interface with which to receive update notifications.
        /// </summary>
        /// <param name="instrumentSymbol">The instrument's unique symbol</param>
        /// <param name="priceUpdateReceiver">The client-supplied callback interface for notifications</param>
        /// <returns></returns>
        bool SubscribeToPriceUpdates(string instrumentSymbol, IPriceUpdateReceiver priceUpdateReceiver);
        bool UnsubscribeFromPriceUpdates(string instrumentSymbol, IPriceUpdateReceiver priceUpdateReceiver);
    }
}
