using System.Collections.Generic;
using Nethereum.UI.Core.ViewModels;
using Nethereum.Wallet.Model;

namespace Nethereum.UI.Core.Services
{
    public interface IAccountTokenViewModelMapperService
    {
        List<AccountTokenViewModel> Map(List<AccountToken> accountTokens);
        List<AccountTokenViewModel> Map(EthAccountToken ethAccountToken, List<AccountToken> accountTokens);
    }
}