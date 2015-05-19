using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FinancialTradingService;
namespace PriceUpdateConsole
{
    /// <summary>
    /// Demos use of the IFinancialTradingService interface
    /// And gets me writing some C#!!
    /// </summary>
    class Program : IPriceUpdateReceiver
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }
        void Run()
        {
            // Create service object
            var tradingService = new DemoFinancialTradingService();
            bool serviceUp = tradingService.IsServiceUp;
            Console.WriteLine("Financial trading service started - console\n\n");

            // Create a session, show available instruments
            var session = tradingService.CreateSession("duncan", "pass");
            Console.WriteLine("The following instruments are available:");
            foreach (var instrumentDefinition in session.GetInstruments(null))
            {
                Console.WriteLine("{0} \t\t {1}", instrumentDefinition.Symbol, instrumentDefinition.Description);
            }  
            
            Console.WriteLine("Enter Q to quit, or an instrument symbol to begin watching price updates.");
            while (serviceUp)
            {
                Console.Write("Watch (Q to quit):", tradingService.CurrentStatus.ToString());
                string response = Console.ReadLine();
                if (response == null )
                    continue;
                response = response.ToUpper();
                if (response == "Q")
                {
                    tradingService.Stop();
                    break;
                }
                    

                serviceUp = tradingService.IsServiceUp;
                if (serviceUp )
                {
                    // Is this an instrument supported by the service?

                    // Request to subscribe to the price updates for the given instrument
                    // Ah. 
                    session.SubscribeToPriceUpdates(response, this);
                }
                
            }
        }

        public void OnPriceUpdate(IPriceUpdate update)
        {
            Console.WriteLine("Symbol: {0}   Bid: {1}   Ask: {2}   Timestamp: {3}", update.Instrument, update.BidPrice,
                update.AskPrice, update.ServerTimestamp);
        }
    }
}
