using System.Threading;
using FinancialTradingService.Instrument;
using FinancialTradingService.Model;

namespace FinancialTradingService
{
    /// <summary>
    /// Models a simplified financial trading service.
    /// The service contains a single set of instruments with prices.
    /// These prices must be modified in real-time, randomly, using some multi-threaded approach.
    /// 
    /// </summary>
    public class DemoFinancialTradingService : IFinancialTradingService
    {
        /// <summary>
        /// Private test data for loading Instrument Catalogue
        /// </summary>
        /// 
        private static readonly IInstrument[] _stockInstruments =
        {
            new StockInstrument("MSFT", "Microsoft Corp.", 48.2m),
            new StockInstrument("AAPL", "Apple Inc.", 128.7m),
            new StockInstrument("GOOG", "Google Inc.", 533.8m),
            new StockInstrument("FB", "Facebook Inc.", 80.4m),
            new StockInstrument("INTC", "Intel Corporation", 32.9m)

        };

        private Thread _updateThread;

        public DemoFinancialTradingService()
        {
            // Initialise the instrument catalogue
            // (1) Add some stocks
            foreach (var i in _stockInstruments)
                InstrumentCatalogue.Add(i);
            // (2) TODO: Add some Forex
            // (3) Add CFDs

            // Spin up our price adjustment thread. This thread alters the prices of our instruments randomly.
            _updateThread = new Thread(PriceChangeSimulator.Run) {Name = "Price adjustment thread"};
            _updateThread.Start();

        }

        public void Stop()
        {
            PriceChangeSimulator.Stop();
        }

        public ServiceStatus CurrentStatus
        {
            get; private set;
        }

        public bool IsServiceUp
        {
            get { return CurrentStatus == ServiceStatus.UpAndRunning || CurrentStatus == ServiceStatus.Busy; }
            
        }

        
        public IFinancialTradingSession CreateSession(string username, string password)
        {
            // In practice, we'd be talking to some remote host
            // We're hard-coded for the user called 'dmackay' with password 'pass'
            if (username == "duncan" && password == "pass")
            {
                var session = new DemoFinancialTradingSession();
                CurrentStatus = ServiceStatus.UpAndRunning;
                return session;
            }

            return null;
        }
    }
}
