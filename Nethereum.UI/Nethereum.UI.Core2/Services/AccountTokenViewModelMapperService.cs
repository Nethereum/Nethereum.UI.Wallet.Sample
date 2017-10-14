using System;
using System.Collections.Generic;
using System.Linq;
using Nethereum.UI.Core.ViewModels;
using Nethereum.Wallet.Model;
using Nethereum.Wallet.Services;

namespace Nethereum.UI.Core.Services
{
    public class AccountSummaryViewModelMapperService : IAccountSummaryViewModelMapperService
    {
        public List<AccountSummaryViewModel> Map(List<AccountInfo> accountsInfo)
        {
            return (from accountInfo in accountsInfo
                select new AccountSummaryViewModel()
                {
                    BalanceSummary = "Eth: " + accountInfo.Eth.Balance,
                    Address = accountInfo.Address
                }).ToList();
        } 
    }

    public class AccountTokenViewModelMapperService: IAccountTokenViewModelMapperService
    {
        private readonly ITokenRegistryService tokenRegistryService;

        public AccountTokenViewModelMapperService(ITokenRegistryService tokenRegistryService)
        {
            this.tokenRegistryService = tokenRegistryService;
        }

        public List<AccountTokenViewModel> Map(List<AccountToken> accountTokens)
        {
            var tokens = new List<Token>();
            tokens.AddRange(tokenRegistryService.GetRegisteredTokens());
            tokens.Add(new EthToken());

            return (from accountToken in accountTokens
                let token = tokens.FirstOrDefault(x => x.Symbol == accountToken.Symbol)
                select new AccountTokenViewModel
                {
                    Balance = accountToken.Balance, Symbol = accountToken.Symbol, TokenName = token.Name, TokenImgUrl = token.ImgUrl
                }).ToList();
        }


        public List<AccountTokenViewModel> Map(EthAccountToken ethAccountToken, List<AccountToken> accountTokens)
        {
            accountTokens.Insert(0, ethAccountToken);
            return Map(accountTokens);
        }
    }
}