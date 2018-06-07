using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.Wallet.Services
{
    public class InMemoryAccountRegistryService : IAccountRegistryService
    {
        public List<string> Accounts { get; set; }

        public InMemoryAccountRegistryService()
        {
            Accounts = new List<string>(GetDefaultAccounts());
        }

        public List<string> GetDefaultAccounts()
        {
            return new List<string>(new string[]
            {
                "0x12890d2cce102216644c59daE5baed380d84830c", // Test account with private keys
                "0x627306090abab3a6e1400e9345bc60c78a8bef57", // MKR Rinkeby for display balances
                "0xc0d6dda38239e5b777e6026148b1c7bff1e061e3", //DGD Rinkeby for dislplay balances
            });

        }

        public List<string> GetRegisteredAccounts()
        {
            return Accounts;
        }

        public async Task RegisterAccountAddress(string address)
        {
            if (!Accounts.Exists(x => x.ToLower() == address.ToLower()))
            {
                Accounts.Add(address);
            }
        }
    }
}