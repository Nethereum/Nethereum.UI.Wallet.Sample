using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Nethereum.UI.Core.Services;
using Nethereum.Wallet.Services;
using Xamarin.Forms;

namespace Nethereum.UI.Core.ViewModels
{
    public class BalanceSummaryViewModel : BaseViewModel
    {
        private readonly IEthWalletService walletService;
        private readonly IAccountTokenViewModelMapperService accountTokenViewModelMapperService;

        private AccountTokenViewModel selectedToken;

        private ObservableCollection<AccountTokenViewModel> tokenBalanceSummary;

        public BalanceSummaryViewModel(IEthWalletService walletService,
            IAccountTokenViewModelMapperService accountTokenViewModelMapperService
            )
        {
            this.walletService = walletService;
            this.accountTokenViewModelMapperService = accountTokenViewModelMapperService;
            tokenBalanceSummary = new ObservableCollection<AccountTokenViewModel>();
            Title = "Balances";
            Icon = "blog.png";
        }

        

        public ObservableCollection<AccountTokenViewModel> TokensBalanceSummary
        {
            get { return tokenBalanceSummary; }
            set
            {
                tokenBalanceSummary = value;
                RaisePropertyChanged(() => TokensBalanceSummary);
            }
        }

       

        public AccountTokenViewModel SelectedToken
        {
            get { return selectedToken; }
            set
            {
                selectedToken = value;
                RaisePropertyChanged(() => SelectedToken);

                // ShowSelectedAccountCommand.Execute(null);
            }
        }


        //public ICommand ShowSelectedAccountCommand
        //{
        //    get
        //    {
        //        return null;
        //        ////return new MvxCommand(() => ShowViewModel<DetailedMovieViewModel>(new { movieId = SelectedAccount.Id }),
        //        ////    () => SelectedAccount != null);
        //    }
        //}

        /// <summary>
        ///     Command to load/refresh items
        /// </summary>
        public ICommand LoadItemsCommand
        {
            get { return new MvxAsyncCommand(LoadData); }
        }

        public override async void Start()
        {
            await LoadData();
            base.Start();
        }


        private async Task LoadData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;
            try
            {
                var walletSummary = await walletService.GetWalletSummary();
                TokensBalanceSummary.Clear();
                foreach (var accountSummary in accountTokenViewModelMapperService.Map(walletSummary.EthBalanceSummary,
                walletSummary.TokenBalanceSummary))
                {
                    TokensBalanceSummary.Add(accountSummary);
                }
            }
            catch
            {
                error = true;
            }

            if (error)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load token summary", "OK");
            }

            IsBusy = false;
        }
    }
}