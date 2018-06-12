using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Nethereum.Signer;
using Nethereum.StandardTokenEIP20.CQS;
using Nethereum.Util;
using Nethereum.Wallet.Model;
using Nethereum.Wallet.Services;
using ReactiveUI;
using Xamarin.Forms;

namespace Nethereum.UI.Core.ViewModels
{
    public class TransferTokenViewModel : BaseViewModel
    {
        private readonly IAccountRegistryService _accountRegistryService;

        protected readonly Interaction<string, bool> _confirmTransfer;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITokenRegistryService _tokenRegistryService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ITransactionSenderService _transactionSenderService;
        private readonly IEthWalletService _walletService;

        private decimal _amount;
        private decimal _currentTokenBalance;
        private ulong? _gas;

        private decimal _gasPrice;
        private ObservableCollection<string> _registeredAccounts;
        private ObservableCollection<ContractToken> _registeredTokens;

        private int? _selectedAccountFrom;

        private int? _selectedToken;
        private string _toAddress;
        private string _symbol;

        public TransferTokenViewModel(IEthWalletService walletService,
            ITokenRegistryService tokenRegistryService,
            IAccountRegistryService accountRegistryService,
            ITransactionSenderService transactionSenderService,
            IMvxNavigationService navigationService,
            ITransactionHistoryService transactionHistoryService
        )
        {
            _walletService = walletService;
            _tokenRegistryService = tokenRegistryService;
            _accountRegistryService = accountRegistryService;
            _transactionSenderService = transactionSenderService;
            _navigationService = navigationService;
            _transactionHistoryService = transactionHistoryService;
            _registeredAccounts = new ObservableCollection<string>();
            _registeredTokens = new ObservableCollection<ContractToken>();
            _confirmTransfer = new Interaction<string, bool>();

            _selectedToken = -1;
            _selectedAccountFrom = -1;
            _gasPrice = Web3.Web3.Convert.FromWei(TransactionBase.DEFAULT_GAS_PRICE, UnitConversion.EthUnit.Gwei);

            Title = "Transfer Token";
            Icon = "ethIcon.png";

            this.WhenAnyValue(x => x.SelectedToken,
                x => x.SelectedAccountFrom, (selectedToken, selectedAccountFrom) => selectedToken != -1 &&
                                                                                    selectedAccountFrom != -1).Subscribe(async _=>
                await RefreshTokenBalanceAsync());

            var canExecuteTransaction = this.WhenAnyValue(
                x => x.ToAddress,
                x => x.Amount,
                x => x.SelectedToken,
                x => x.SelectedAccountFrom,
                (addressTo, amount, selectedToken, selectedAccountFrom) =>
                    !string.IsNullOrEmpty(ToAddress) && //todo valid address
                    amount > 0  &&
                    selectedToken != -1 &&
                    selectedAccountFrom != -1);

            _executeTransactionCommand = ReactiveCommand.CreateFromTask(ExecuteAsync, canExecuteTransaction);
        }

        public ObservableCollection<string> RegisteredAccounts
        {
            get => _registeredAccounts;
            set { this.RaiseAndSetIfChanged(ref _registeredAccounts, value); }
        }

        public ObservableCollection<ContractToken> RegisteredTokens
        {
            get => _registeredTokens;
            set { this.RaiseAndSetIfChanged(ref _registeredTokens, value); }
        }

        public int? SelectedToken
        {
            get => _selectedToken;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedToken, value);
            }
        }

        public int? SelectedAccountFrom
        {
            get => _selectedAccountFrom;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedAccountFrom, value);
            }
        }

        public Interaction<string, bool> ConfirmTransfer => _confirmTransfer;

        public ICommand RefreshTokenBalanceCommand
        {
            get { return new MvxAsyncCommand(async () => await RefreshTokenBalanceAsync()); }
        }

        public string Symbol
        {
            get => _symbol;
            set { this.RaiseAndSetIfChanged(ref _symbol, value); }
        }

        public string ToAddress
        {
            get => _toAddress;
            set { this.RaiseAndSetIfChanged(ref _toAddress, value); }
        }

        public decimal Amount
        {
            get => _amount;
            set { this.RaiseAndSetIfChanged(ref _amount, value); }
        }

        public ulong? Gas
        {
            get => _gas;
            set { this.RaiseAndSetIfChanged(ref _gas, value); }
        }

        public decimal GasPrice
        {
            get => _gasPrice;
            set { this.RaiseAndSetIfChanged(ref _gasPrice, value); }
        }


        public decimal CurrentTokenBalance
        {
            get => _currentTokenBalance;
            set { this.RaiseAndSetIfChanged(ref _currentTokenBalance, value); }
        }

        protected ReactiveCommand<Unit, Unit> _executeTransactionCommand;
        public ReactiveCommand<Unit, Unit> ExecuteTransactionCommand => this._executeTransactionCommand;

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadDataAsync();
        }

        private async Task RefreshTokenBalanceAsync()
        {
            if (SelectedToken != -1 && SelectedAccountFrom != -1)
                CurrentTokenBalance = await _walletService.GetTokenBalance(RegisteredTokens[SelectedToken.Value],
                    RegisteredAccounts[SelectedAccountFrom.Value]);
        }

        private async Task LoadDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;
            try
            {
                _registeredAccounts.Clear();
                _accountRegistryService.GetRegisteredAccounts().ForEach(x => _registeredAccounts.Add(x));

                _tokenRegistryService.GetRegisteredTokens().ForEach(x => _registeredTokens.Add(x));
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load registered accounts or tokens", "OK");
            }

            IsBusy = false;
        }


        public string GetConfirmationMessage()
        {
            return
                $"Are you sure you want to make this transfer: \n\r To: {ToAddress} \n\r Token Amount: {Amount}";
        }

        private async Task ExecuteAsync()
        {

            var confirmed = await _confirmTransfer.Handle(GetConfirmationMessage());
            if (confirmed)
            {
                var error = false;
                var exceptionMessage = "";
               
                    var currentToken = RegisteredTokens[SelectedToken.Value];
                    var currentAddres = RegisteredAccounts[SelectedAccountFrom.Value];
                   
                        try
                        {
                            var transferFunction = new TransferFunction
                            {
                                Value = Web3.Web3.Convert.ToWei(Amount, currentToken.NumberOfDecimalPlaces),
                                FromAddress = currentAddres,
                                Gas = Gas,
                                GasPrice = Web3.Web3.Convert.ToWei(GasPrice, UnitConversion.EthUnit.Gwei),
                                To = ToAddress
                            };

                            var transactionHash = await
                                _transactionSenderService.SendTransactionAsync(transferFunction, currentToken.Address);
                            await _transactionHistoryService.AddTransaction(transactionHash);
                            
                        }
                        catch (Exception ex)
                        {
                            error = true;
                            exceptionMessage = ex.Message;
                        }

                        if (error)
                        {
                            //todo rxui
                            var page = new ContentPage();
                            await page.DisplayAlert("Error", "Unable to transfer token :" + exceptionMessage, "OK");
                        }

                        IsBusy = false;          
            }
        }
    }
}