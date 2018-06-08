using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using ReactiveUI;

namespace Nethereum.Wallet.Services
{

    public class TransactionViewModel : ReactiveObject
    {

        public const string STATUS_INPROGRESS = "Pending";
        public const string STATUS_COMPLETED = "Completed";

        public void Initialise(Transaction transaction)
        {
            this.TransactionHash = transaction.TransactionHash;
            this.BlockHash = transaction.BlockHash;
            this.Nonce = (ulong)transaction.Nonce.Value;
            this.From = transaction.From;
            this.To = transaction.To;
            this.Gas = (ulong)transaction.Gas.Value;
            this.GasPrice = (ulong)transaction.GasPrice.Value;
            this.Data = transaction.Input;

            if (transaction.Value != null) this.Amount = Web3.Web3.Convert.FromWei(transaction.Value.Value);
        }

        private string _blockHash;
        public string BlockHash
        {
            get => _blockHash;
            set => this.RaiseAndSetIfChanged(ref _blockHash, value);
        }

        private string _transactionHash;
        public string TransactionHash
        {
            get => _transactionHash;
            set => this.RaiseAndSetIfChanged(ref _transactionHash, value);
        }

        private string _from;
        public string From
        {
            get => _from;
            set => this.RaiseAndSetIfChanged(ref _from, value);
        }

        private string _to;
        public string To
        {
            get => _to;
            set => this.RaiseAndSetIfChanged(ref _to, value);
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        private ulong? _gas;
        public ulong? Gas
        {
            get => _gas;
            set => this.RaiseAndSetIfChanged(ref _gas, value);
        }

        private string _data;
        public string Data
        {
            get => _data;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }

        private ulong? _gasPrice;
        public ulong? GasPrice
        {
            get => _gasPrice;
            set => this.RaiseAndSetIfChanged(ref _gasPrice, value);
        }

        private ulong _nonce;
        public ulong Nonce
        {
            get => _nonce;
            set => this.RaiseAndSetIfChanged(ref _nonce, value);
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
    }

    public class TransactionHistoryService: ITransactionHistoryService
    {
        private readonly IWalletConfigurationService _walletConfigurationService;
        public ReactiveList<TransactionViewModel> Transactions { get; set; } = new ReactiveList<TransactionViewModel>();
        private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(2000);
        private IDisposable timer;
        private readonly object receiptsCheckLock = new object();

        public TransactionHistoryService(IWalletConfigurationService walletConfigurationService)
        {
            _walletConfigurationService = walletConfigurationService;
            timer = Observable.Timer(updateInterval, updateInterval, RxApp.MainThreadScheduler)
                .Subscribe(async _ => await CheckReceiptsAsync());
        }

        public async Task AddTransaction(string transactionHash)
        {

                var web3 = _walletConfigurationService.GetReadOnlyWeb3();
                var transactionViewModel = new TransactionViewModel();
                var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
                transactionViewModel.Initialise(transaction);
                transactionViewModel.Status = TransactionViewModel.STATUS_INPROGRESS;
                lock (receiptsCheckLock)
                {
                    Transactions.Add(transactionViewModel);
                }
        }

        public async Task CheckReceiptsAsync()
        {
            IEnumerable<TransactionViewModel> transactionsInProgress = new List<TransactionViewModel>();

            lock (receiptsCheckLock)
            {
                transactionsInProgress =
                    Transactions.Where(x => x.Status == TransactionViewModel.STATUS_INPROGRESS);
            }

            var web3 = _walletConfigurationService.GetReadOnlyWeb3();
           
            foreach (var transaction in transactionsInProgress)
            {
                var receipt = await web3.Eth.Transactions.GetTransactionReceipt
                    .SendRequestAsync(transaction.TransactionHash);

                if (receipt != null)
                {
                    transaction.BlockHash = receipt.BlockHash;
                    transaction.Status = TransactionViewModel.STATUS_COMPLETED;
                }
            } 
        }
    }

    public interface ITransactionHistoryService
    {
        Task AddTransaction(string transactionHash);
        ReactiveList<TransactionViewModel> Transactions { get; set; }
    }
}
