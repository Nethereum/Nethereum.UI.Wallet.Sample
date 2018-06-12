using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using Nethereum.StandardTokenEIP20.CQS;
using Nethereum.Util;
using Nethereum.Wallet.Model;
using Nethereum.Wallet.Services;
using Xamarin.Forms;

namespace Nethereum.UI.Core.ViewModels
{
    public class TransferTokenViewModel : BaseViewModel
    {
        private readonly IEthWalletService _walletService;
        private readonly ITokenRegistryService _tokenRegistryService;
        private readonly IAccountRegistryService _accountRegistryService;
        private readonly ITransactionSenderService _transactionSenderService;
        private readonly IMvxNavigationService _navigationService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private ObservableCollection<string> _registeredAccounts;
        private ObservableCollection<ContractToken> _registeredTokens;

        private int? _selectedAccountFrom;

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

            _selectedToken = -1;
            _selectedAccountFrom = -1;
            _gasPrice = Web3.Web3.Convert.FromWei(Transaction.DEFAULT_GAS_PRICE, UnitConversion.EthUnit.Gwei);

            Title = "Transfer Token";
            Icon = "ethIcon.png";
        }

        public ObservableCollection<string> RegisteredAccounts
        {
            get => _registeredAccounts;
            set
            {
                _registeredAccounts = value;
                RaisePropertyChanged(() => RegisteredAccounts);
            }
        }

        public ObservableCollection<ContractToken> RegisteredTokens
        {
            get => _registeredTokens;
            set
            {
                _registeredTokens = value;
                RaisePropertyChanged(() => RegisteredTokens);
            }
        }

        public int? SelectedToken
        {
            get => _selectedToken;
            set
            {
                _selectedToken = value;
                RaisePropertyChanged(() => SelectedToken);
                RefreshTokenBalanceCommand.Execute(null);
            }
        }

        public int? SelectedAccountFrom
        {
            get => _selectedAccountFrom;
            set
            {
                _selectedAccountFrom = value;
                RaisePropertyChanged(() => SelectedAccountFrom);
                RefreshTokenBalanceCommand.Execute(null);
            }
        }

        public ICommand RefreshTokenBalanceCommand
        {
            get { return new MvxAsyncCommand(async () => await RefreshTokenBalance()); }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadData();
        }

        private async Task RefreshTokenBalance()
        {
            if (SelectedToken != -1 && SelectedAccountFrom != -1)
            {
                CurrentTokenBalance =  await _walletService.GetTokenBalance(RegisteredTokens[SelectedToken.Value], RegisteredAccounts[SelectedAccountFrom.Value]);
            }
        }

        private async Task LoadData()
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


        private string symbol;

        public string Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                RaisePropertyChanged(() => Symbol);
            }
        }

        public string ToAddress
        {
            get => _toAddress;
            set
            {
                _toAddress = value;
                RaisePropertyChanged(() => ToAddress);
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        public ulong? Gas
        {
            get => _gas;
            set
            {
                _gas = value;
                RaisePropertyChanged(() => Gas);
            }
        }

        public decimal GasPrice
        {
            get => _gasPrice;
            set
            {
                _gasPrice = value;
                RaisePropertyChanged(() => GasPrice);
            }
        }


        public decimal CurrentTokenBalance
        {
            get => _currentTokenBalance;
            set
            {
                _currentTokenBalance = value;
                RaisePropertyChanged(() => CurrentTokenBalance);
            }
        }

        private string _toAddress;

        private ulong? _gas;

        private decimal _gasPrice;

        private decimal _amount;

        private decimal _currentTokenBalance;

        private int? _selectedToken;

        public ICommand TransferTokenCommand
        {
            get { return new MvxAsyncCommand(async () => await TransferToken()); }
        }

        private async Task TransferToken()
        {
            var error = false;
            var exceptionMessage = "";
            //this needs rxui
            if (SelectedToken != -1 && SelectedAccountFrom != -1 && Amount > 0 && !string.IsNullOrEmpty(ToAddress))
            {
                var currentToken = RegisteredTokens[SelectedToken.Value];
                var currentAddres = RegisteredAccounts[SelectedAccountFrom.Value];
                //todo rxui
                var alertPrompt = new ContentPage();
                var alertPromptResult = true;

                    //await alertPrompt.DisplayAlert("Token Transfer",
                    //"Are you sure you want to transfer, " + Amount + "  " + currentToken.Symbol + " To Address: " +
                    //ToAddress,
                    //"OK", "Cancel");

                if (alertPromptResult)
                {
                    try
                    {

                        var transferFunction = new TransferFunction()
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
                    catch(Exception ex)
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
}