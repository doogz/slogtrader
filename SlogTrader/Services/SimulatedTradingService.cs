using System;

namespace SlogTrader.Services
{
    /// <summary>
    /// class: SimulatedTradingService
    /// Models an online financial trading service.
    /// Users must log on to the service, whereupon they are provided an ??ISlogTrader?? interface
    /// 
    /// </summary>
    class SimulatedTradingService //: IInstrumentTradingService
    {

        /// <summary>
        /// 
        /// </summary>
        public SimulatedTradingService()
        {
            createForexInstruments();
        }

        /// <summary>
        /// 
        /// </summary>
        private void createForexInstruments()
        {
            // Each currency is paired with every other currency - once as the base currency and once more as the quote currency
            foreach (var baseCurr in Model.Currency.Majors)
                foreach (var quoteCurr in Model.Currency.Majors)
                    if (baseCurr != quoteCurr)
                        addForexPair(baseCurr.Code, quoteCurr.Code);
            // todo: 
        }

        private void addForexPair(string baseCurr, string quoteCurr)
        {
            // Two currencies form two instruments e.g. GBPUSD and USDGBP.
            // These instruments are priced as each other's reciprocal i.e. GBPUSD = 1/USDGBP
            string id = baseCurr + quoteCurr;
            string desc = Model.Currency.GetDescription(baseCurr)+"/"+Model.Currency.GetDescription(quoteCurr);
            //var inst = new Model.Instrument(id, desc, Model.Instrument.Market.Forex, 

            
        }
    }


        /*
        private bool _loggedIn = false;
        private IPriceUpdateService _updateSubscriptionService = new SimulatedPricingService();

        public ITradingAccount Login(string username, string password)
        {
            if (username != "dmackay")
            {
                return LoginResult.AccountUnknown;
            }

            if (password != "pass")
            {
                return LoginResult.InvalidPassword;
            }
            
            // Randomly make the login fail now and again
            var rng = new Random();
            if (rng.Next(100) < 5)
            {
                return LoginResult.ServiceNotAvailable;
            }
            _loggedIn = true;
            return LoginResult.Success;
        }

        public IPriceUpdateService GetPriceUpdateService()
        {
            return _updateSubscriptionService;
        }
        public bool IsAlive()
        {
            return _loggedIn;

        }
        public int AccountNumber
        {
            get { return 16452341; }
        }
        public bool Logout()
        {
            return true;
        }
    }
    */
}
