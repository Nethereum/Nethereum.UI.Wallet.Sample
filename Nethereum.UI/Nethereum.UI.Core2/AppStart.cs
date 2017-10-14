using MvvmCross.Core.ViewModels;
using Nethereum.UI.Core.ViewModels;
using Nethereum.Wallet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.UI.Core
{
    /// <summary>
    /// A class that implements the IMvxAppStart interface and can be used to customise
    /// the way an application is initialised
    /// </summary>
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// The login service.
        /// </summary>
        private readonly IWalletConfigurationService walletConfiguration;

        public AppStart(IWalletConfigurationService walletConfiguration)
        {
            this.walletConfiguration = walletConfiguration;
        }

        /// <summary>
        /// Start is called on startup of the app
        /// Hint contains information in case the app is started with extra parameters
        /// </summary>
        public void Start(object hint = null)
        {
            if (walletConfiguration.IsConfigured())
            {
                ShowViewModel<ShellViewModel>();
            }
            else
            {
                //ShowViewModel<WalletConfigurationViewModel>();
            }
        }

    }
}
