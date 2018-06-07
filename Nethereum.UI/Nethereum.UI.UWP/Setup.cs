using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;
using Nethereum.UI.Core;
using Nethereum.UI.UWP.Services;
using Nethereum.Wallet.Services;

namespace Nethereum.UI.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, FormsApp>
    {
        
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Nethereum.UI.Core.App.IsWindows10 = true;
            //Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }
    }
}