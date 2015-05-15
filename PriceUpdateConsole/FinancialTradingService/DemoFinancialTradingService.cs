using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DemoFinancialTradingService()
        {
            // Spin up our price adjustment thread. This thread alters the prices of our instruments randomly.

            
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

        /// I've chosen a fully-fledged separate thread object for the implementation, really just to take a look at System.Threading a bit,
        /// although I think it's a reasonable design choice.
        /// 
        /// I know I can have time from the threads in the thread pool, driven by a timer - and that that might have been easier.
        ///
        /// But this thread will live as long as the service does, it has a single dedicated purpose (and thus a dedicated name too), and I deem it
        /// worthy of its own thread.
        /// 

    }
}
