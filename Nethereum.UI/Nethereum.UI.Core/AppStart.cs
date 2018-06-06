using Nethereum.UI.Core.ViewModels;
using Nethereum.Wallet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Nethereum.UI.Core
{

    public class AppStart<TViewModel> : MvxAppStart<TViewModel> where TViewModel: IMvxViewModel
    {
 
        private readonly IWalletConfigurationService walletConfiguration;
  

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IWalletConfigurationService walletConfiguration) : base(application, navigationService)
        {
            this.walletConfiguration = walletConfiguration;
           
        }

        protected override void NavigateToFirstViewModel(object hint = null)
        {
            if (walletConfiguration.IsConfigured())
            {
                NavigationService.Navigate<TViewModel>().ConfigureAwait(false);

                //navigationService.Navigate<BalanceSummaryViewModel>();

            }
            else
            {
                // NavigationService.Navigate<WalletConfigurationViewModel>();
            }
        }
    }
}
