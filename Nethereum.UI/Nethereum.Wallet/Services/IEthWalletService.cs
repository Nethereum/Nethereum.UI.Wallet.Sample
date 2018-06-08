using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;

namespace Nethereum.Wallet.Services
{
    public interface IEthWalletService
    {
        Task<string[]> GetAccounts();

        Task<AccountInfo> GetAccountInfo(string accountAddress, bool forceRefresh= false);

        Task<List<AccountInfo>> GetAllAccountsInfo(bool forceRefresh = false);

        void InvalidateCache();

        Task<List<WalletTransaction>> GetLatestTransactions();

        Task<WalletSummary> GetWalletSummary(bool forceRefresh = false);
        Task<decimal> GetTokenBalance(ContractToken token, string accountAddress);
        Task<decimal> GetEthBalance(string accountAddress);
    }
}