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
        public async Task<AccountInfo> GetAccountInfo(string accountAddress, bool forceRefresh= false)
        {
            var accountInfo = await GetAllAccountsInfo(forceRefresh);
            return accountInfo.FirstOrDefault(x => x.Address.ToLower() == accountAddress.ToLower());
        }

        public async Task<WalletSummary> GetWalletSummary(bool forceRefresh = false)
        {
            var accounstInfo = await GetAllAccountsInfo(forceRefresh);
            return new WalletSummary(accounstInfo);            
        }

        public async Task<string[]> GetAccounts()
        {
            return accountRegistryService.Accounts.ToArray();
        }

        public async Task<decimal> GetTokenBalance(ContractToken token, string accountAddress)
        {
            var service = new StandardTokenEIP20.StandardTokenService(GetWeb3(), token.Address);
            var wei = await service.GetBalanceOfAsync<BigInteger>(accountAddress).ConfigureAwait(false);
            return Web3.Web3.Convert.FromWei(wei, token.NumberOfDecimalPlaces);
        }

        public async Task<decimal> GetEthBalance(string accountAddress)
        {
            var web3 = GetWeb3();
            var weiBalance = await web3.Eth.GetBalance.SendRequestAsync(accountAddress).ConfigureAwait(false);
            return Web3.Web3.Convert.FromWei(weiBalance);
        }

        public void InvalidateCache()
        {
            lock (lockingObject)
            {
                AccountsInfoInMemoryCache = null;
            }
        }

        public async Task<List<AccountInfo>> RefreshAllAccountsInfo()
        {
            
            var accounts = await GetAccounts();
            var accountsInfo = accounts.Select(x => new AccountInfo() { Address = x }).ToList();
            foreach (var accountInfo in accountsInfo)
            {
                try
                {
                    accountInfo.Eth.Balance = await GetEthBalance(accountInfo.Address);

                    foreach (var token in tokenRegistryService.GetRegisteredTokens())
                    {
                        var accountToken = new AccountToken
                        {
                            Symbol = token.Symbol,
                            Balance = await GetTokenBalance(token, accountInfo.Address)
                        };
                       
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

        private List<AccountInfo> AccountsInfoInMemoryCache { get; set; }

        public async Task<List<AccountInfo>> GetAllAccountsInfo(bool forceRefresh=false)
        {
                if (forceRefresh || AccountsInfoInMemoryCache == null)
                {
                    var accountInfo = await RefreshAllAccountsInfo();
                    lock (lockingObject)
                    {
                        AccountsInfoInMemoryCache = accountInfo;
                    }
                } 
            return AccountsInfoInMemoryCache;
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