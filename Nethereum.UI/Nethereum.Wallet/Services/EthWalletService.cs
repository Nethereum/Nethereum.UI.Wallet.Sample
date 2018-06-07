using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Wallet.Model;

namespace Nethereum.Wallet.Services
{

    public class EthWalletService : IEthWalletService
    {
        private IWalletConfigurationService configuration;
        private ITokenRegistryService tokenRegistryService;
        private readonly IAccountRegistryService accountRegistryService;
        private object lockingObject = new Object();

        public EthWalletService(IWalletConfigurationService configuration, 
            ITokenRegistryService tokenRegistryService, 
            IAccountRegistryService accountRegistryService)
        {
            this.configuration = configuration;
            this.tokenRegistryService = tokenRegistryService;
            this.accountRegistryService = accountRegistryService;
        }
        public async Task<AccountInfo> GetAccountInfo(string accountAddress)
        {
            return null;
        }

        public async Task<WalletSummary> GetWalletSummary(bool forceRefresh = false)
        {
            var accounstInfo = await GetAccountsInfo(forceRefresh);
            return new WalletSummary(accounstInfo);            
        }

        public async Task<string[]> GetAccounts()
        {
            return accountRegistryService.Accounts.ToArray();
        }

        public void InvalidateCache()
        {
            lock (lockingObject)
            {
                AccountInfoInMemoryCache = null;
            }
        }

        public async Task<List<AccountInfo>> RefreshAccountInfo()
        {
            var web3 = GetWeb3();
            var accounts = await GetAccounts();
            var accountsInfo = accounts.Select(x => new AccountInfo() { Address = x }).ToList();
            foreach (var accountInfo in accountsInfo)
            {
                try
                {
                    var weiBalance = await web3.Eth.GetBalance.SendRequestAsync(accountInfo.Address).ConfigureAwait(false);
                    var balance = (decimal)weiBalance.Value / (decimal)Math.Pow(10, 18);
                    accountInfo.Eth.Balance = balance;

                    foreach (var token in tokenRegistryService.GetRegisteredTokens())
                    {
                        var service = new StandardTokenEIP20.StandardTokenService(web3, token.Address);
                        var accountToken = new AccountToken
                        {
                            Symbol = token.Symbol
                        };
                        var wei = await service.GetBalanceOfAsync<BigInteger>(accountInfo.Address);
                        balance = (decimal)wei / (decimal)Math.Pow(10, token.NumberOfDecimalPlaces);
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

        private List<AccountInfo> AccountInfoInMemoryCache { get; set; }

        public async Task<List<AccountInfo>> GetAccountsInfo(bool forceRefresh=false)
        {
                if (forceRefresh || AccountInfoInMemoryCache == null)
                {
                    var accountInfo = await RefreshAccountInfo();
                    lock (lockingObject)
                    {
                        AccountInfoInMemoryCache = accountInfo;
                    }
                }
            
            return AccountInfoInMemoryCache;
        }

        public Task<List<WalletTransaction>> GetLatestTransactions()
        {
            throw new NotImplementedException();
        }

        private Web3.Web3 GetWeb3()
        {
            return new Web3.Web3(configuration.ClientUrl);
        }
    }
}