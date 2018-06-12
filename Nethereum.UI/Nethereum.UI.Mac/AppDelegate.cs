using AppKit;
using Foundation;
using MvvmCross.Forms.Platforms.Mac.Core;
using Xamarin.Forms.Platform.MacOS;
using Nethereum.UI.Core;
using MvvmCross;
using Nethereum.Wallet;
namespace Nethereum.UI.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, Core.FormsApp>
    {
    }

    public class Setup:MvxFormsMacSetup<Core.App, Core.FormsApp>
    {
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            //Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

           // Mvx.RegisterSingleton<Core.Services.ILocalizeService>(new Services.LocalizeService());
        }
        
    }
}
