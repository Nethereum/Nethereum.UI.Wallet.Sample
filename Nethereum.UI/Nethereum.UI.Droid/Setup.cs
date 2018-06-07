using Android.Content;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Presenters;
using Nethereum.UI.Core;
using Nethereum.Wallet.Services;
using Nethereum.UI.Droid.Services;

namespace Nethereum.UI.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, FormsApp>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
           // Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(new Services.LocalizeService());
        }
       
    }
}