
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System;
using Nethereum.JsonRpc.Client;

namespace Nethereum.UI.Core.Services
{

    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }

    
}
