using Nethereum.Wallet.Services;
using ReactiveUI;

namespace Nethereum.UI.Core.ViewModels
{
    public class TransactionHistoryViewModel : BaseViewModel
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryViewModel(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
            Title = "Transactions";
        }

        public ReactiveList<TransactionViewModel> Transactions => _transactionHistoryService.Transactions;
    }
}