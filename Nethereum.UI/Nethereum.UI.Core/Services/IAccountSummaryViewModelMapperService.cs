using System.Collections.Generic;
using Nethereum.UI.Core.ViewModels;
using Nethereum.Wallet.Model;

namespace Nethereum.UI.Core.Services
{
    public interface IAccountSummaryViewModelMapperService
    {
        List<AccountSummaryViewModel> Map(List<AccountInfo> accountsInfo);
    }
}