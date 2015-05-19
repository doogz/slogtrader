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

        public IInstrument[] GetInstruments(string filter)
        {
            return InstrumentCatalogue.ToArray();
        }

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
            {
                Console.WriteLine("Instrument {0} is not available.", instrument);
                return false;
            }
           
            checkInst.SubscribeToPriceUpdates(priceUpdateReceiver);
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
            var checkInst = InstrumentCatalogue.Find(instrument);
            if (checkInst == null)
            {
                Console.WriteLine("Instrument {0} is not defined.", instrument);
                return false;
            }

            checkInst.SubscribeToPriceUpdates(priceUpdateReceiver);
            return true;
        }

   

    }
}
