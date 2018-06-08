using System;
using System.Collections.Generic;
using System.Text;using Nethereum.Wallet.Services;

namespace Nethereum.Wallet.Services
{
    public class InMemoryKeySecureStorageService:IAccountKeySecureStorageService
    {
        private Dictionary<string, string> MockSecureStorage { get; set; }

        public InMemoryKeySecureStorageService()
        {
            MockSecureStorage = new Dictionary<string, string>();
            MockSecureStorage.Add("0x12890d2cce102216644c59daE5baed380d84830c".ToLower(), "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7");
        }

        public string GetPrivateKey(string account)
        {
            if (MockSecureStorage.ContainsKey(account.ToLower()))
            {
                return MockSecureStorage[account.ToLower()];
            }

            return null;
        }
    }
}
