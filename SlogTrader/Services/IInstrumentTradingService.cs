using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlogTrader.Services
{
    enum LoginResult
    {
        Success, ServiceNotAvailable, AccountUnknown, InvalidPassword, AccountExpired
    }
    
    //enum ServiceStatus
    //{
    //    LoggedIn, LoggedOut, NotAvailable
    //}

    interface IInstrumentTradingService
    {
        LoginResult Login(string username, string password);
        int AccountNumber { get; }
        bool IsAlive();
        IPriceUpdateService GetPriceUpdateService();
        bool Logout();

    }
}
