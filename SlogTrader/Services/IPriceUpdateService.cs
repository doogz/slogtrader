﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlogTrader.Services
{
    interface IPriceUpdateService
    {
        bool SubscribeToPriceUpdates(string instrument);
        bool Unsubscribe();
    }
}
