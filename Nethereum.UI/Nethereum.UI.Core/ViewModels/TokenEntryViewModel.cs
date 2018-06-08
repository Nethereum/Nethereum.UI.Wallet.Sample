using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Nethereum.Wallet.Model;
using Nethereum.Wallet.Services;
using Xamarin.Forms;

namespace Nethereum.UI.Core.ViewModels
{
    public class TokenEntryViewModel : BaseViewModel
    {
        private readonly IEthWalletService _walletService;
        private readonly ITokenRegistryService _tokenRegistryService;
        private readonly IMvxNavigationService _navigationService;

        public TokenEntryViewModel(IEthWalletService walletService,
            ITokenRegistryService tokenRegistryService,
            IMvxNavigationService navigationService
        )
        {
           
            _walletService = walletService;
            _tokenRegistryService = tokenRegistryService;
            _navigationService = navigationService;

            Title = "Add Token";
            Icon = "blog.png";
            DecimalPlaces = 18;
        }


        private string symbol;

        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                RaisePropertyChanged(() => Symbol);
            }
        }

        public string TokenAddress
        {
            get { return tokenAddress; }
            set
            {
                tokenAddress = value;
                RaisePropertyChanged(() => TokenAddress);
            }
        }

        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set
            {
                decimalPlaces = value;
                RaisePropertyChanged(() => DecimalPlaces);
            }
        }

        public string TokenName
        {
            get { return tokenName; }
            set
            {
                tokenName = value;
                RaisePropertyChanged(() => TokenName);
            }
        }

        public string TokenImgUrl
        {
            get { return tokenImgUrl; }
            set
            {
                tokenImgUrl = value;
                RaisePropertyChanged(() => TokenImgUrl);
            }
        }

        private string tokenAddress;

        private string tokenName;

        private string tokenImgUrl;

        private int decimalPlaces;

        public ICommand SaveTokenCommand
        {
            get { return new MvxAsyncCommand(async () => await SaveToken()); }
        }

        private async Task SaveToken()
        {
            var error = false;

            try
            {
                var contractToken = new ContractToken
                {
                    Address = TokenAddress,
                    Name = TokenName,
                    Symbol = Symbol,
                    NumberOfDecimalPlaces = DecimalPlaces
                };

                await _tokenRegistryService.RegisterToken(contractToken);
                _walletService.InvalidateCache();
                await _navigationService.Navigate<BalanceSummaryViewModel>();
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to register the token", "OK");
            }

            IsBusy = false;
        }
    }
}