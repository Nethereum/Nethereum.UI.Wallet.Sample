using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;

namespace Nethereum.Wallet.Services
{
    public class EthWalletService : IEthWalletService
    {
        private IWalletConfigurationService configuration;
        private ITokenRegistryService tokenRegistryService;

        public EthWalletService(IWalletConfigurationService configuration, ITokenRegistryService tokenRegistryService)
        {
            this.configuration = configuration;
            this.tokenRegistryService = tokenRegistryService;
        }
        public async Task<AccountInfo> GetAccountInfo(string accountAddress)
        {
            return null;
        }

        public async Task<WalletSummary> GetWalletSummary()
        {
            var accounstInfo = await GetAccountsInfo();
            return new WalletSummary(accounstInfo);            
        }

        public async Task<string[]> GetAccounts()
        {
            return configuration.GetAccounts();
            //try
            //{
            //    var web3 = GetWeb3();
            //    //This can look at a local store for account addresses
            //    var accounts = await web3.Eth.Accounts.SendRequestAsync();
            //    return accounts;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
            //return new string[] { };
        }

        public async Task<List<AccountInfo>> GetAccountsInfo()
        {
            var web3 = GetWeb3();
            var accounts = await GetAccounts();
            var accountsInfo = accounts.Select(x => new AccountInfo() { Address = x }).ToList();
            foreach(var accountInfo in accountsInfo)
            {
                try
                {
                    var weiBalance = await web3.Eth.GetBalance.SendRequestAsync(accountInfo.Address);
                    var balance = (decimal) weiBalance.Value/(decimal) Math.Pow(10, 18);
                    accountInfo.Eth.Balance = balance;

                    foreach (var token in tokenRegistryService.GetRegisteredTokens())
                    {
                        var service = new StandardTokenEIP20.StandardTokenService(web3, token.Address);
                        var accountToken = new AccountToken();
                        accountToken.Symbol = token.Symbol;
                        var wei = await service.GetBalanceOfAsync<BigInteger>(accountInfo.Address);
                        balance = (decimal) wei/(decimal) Math.Pow(10, token.NumberOfDecimalPlaces);
                        accountToken.Balance = balance;
                        accountInfo.AccountTokens.Add(accountToken);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return accountsInfo;
        }

        public Task<List<WalletTransaction>> GetLatestTransactions()
        {
            throw new NotImplementedException();
        }

        private Web3.Web3 GetWeb3()
        {
            return new Web3.Web3(configuration.Client);
        }
    }
}