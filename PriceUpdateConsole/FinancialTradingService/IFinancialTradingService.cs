using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService
{
    /// <summary>
    /// Defines client-side abstraction of a financial trading service.
    /// 
    /// Service availability is indicated through the CurrentStatus property.
    /// Interactions take place via authorised sessions, provided by the factory method CreateSession.
    /// Sessions implement IFinancialTradingSession, which directly supports price update subscriptions.
    /// 
 
    /// 
    /// We could actually work out the timestamp differences between our clock and the server's using NTP,
    /// to accurately measure server/network delays.
    /// 

    /// </summary>
    public enum ServiceStatus { UpAndRunning, Busy, Down }

    interface IFinancialTradingService
    {
        /// <summary>
        /// Provides indication of health of connection to server.
        /// The service may indicate 'Busy' when server response times are poor.
        /// (TODO: We could measure this...)
        /// </summary>
        ServiceStatus CurrentStatus { get; }
        
        /// Factory method
        IFinancialTradingSession CreateSession(string username, string password);


    }
}
