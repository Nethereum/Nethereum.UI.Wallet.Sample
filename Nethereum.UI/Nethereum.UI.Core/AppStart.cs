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
    public class AppStart : MvxAppStart
    {
 
        private readonly IWalletConfigurationService walletConfiguration;
        private readonly IMvxNavigationService navigationService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IWalletConfigurationService walletConfiguration) : base(application)
        {
            this.walletConfiguration = walletConfiguration;
            this.navigationService = navigationService;

        }

        protected override void ApplicationStartup(object hint = null)
        {
            base.ApplicationStartup(hint);
            NavigateToFirstViewModel(hint);

        }

        protected void NavigateToFirstViewModel(object hint = null)
        {
            if (walletConfiguration.IsConfigured())
            {
                navigationService.Navigate<MenuViewModel>();
                navigationService.Navigate<BalanceSummaryViewModel>();

            }
            else
            {
                // NavigationService.Navigate<WalletConfigurationViewModel>();
            }
        }
    }
}
