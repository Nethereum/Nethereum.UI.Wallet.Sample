using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;

namespace Nethereum.Wallet.Services
{
    public interface IEthWalletService
    {
        Task<WalletSummary> GetWalletSummary(bool forceRefresh = false);
        Task<string[]> GetAccounts();

        Task<AccountInfo> GetAccountInfo(string accountAddress);

        Task<List<AccountInfo>> GetAccountsInfo(bool forceRefresh = false);

        void InvalidateCache();

        Task<List<WalletTransaction>> GetLatestTransactions();

    }
}