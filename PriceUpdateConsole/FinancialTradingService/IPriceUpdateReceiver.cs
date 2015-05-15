using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingService
{
    /// <summary>
    /// Defines a callback interface for implementation by clients.
    /// The interface defines a single method, called OnPriceUpdate. The details of the update are hidden behind an interface, IPriceUpdate.
    /// That allows extension to the detail (e.g. an IFooBarPriceUpdate) within a price update, without breaking what could be an established API.
    /// </summary>
    public interface IPriceUpdateReceiver
    {
        void OnPriceUpdate(IPriceUpdate update);
    }
}
