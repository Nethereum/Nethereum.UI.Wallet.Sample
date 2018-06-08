using ImageCircle.Forms.Plugin.iOS;
using MvvmCross;
using UIKit;
using Xamarin.Forms;
using Nethereum.Wallet.Services;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using Nethereum.UI.Core;

namespace Nethereum.UI.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, FormsApp>
    {
       
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            //Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(new Services.LocalizeService());
        }
    }
}
