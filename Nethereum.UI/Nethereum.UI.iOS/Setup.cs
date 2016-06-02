using ImageCircle.Forms.Plugin.iOS;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.iOS.Platform;
using UIKit;
using Xamarin.Forms;
using MvvmCross.Forms.Presenter.iOS;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using Nethereum.Wallet.Services;
using Nethereum.UI.UWP.Services;

namespace Nethereum.UI.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(new Services.LocalizeService());
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            Forms.Init();
            ImageCircleRenderer.Init();
            var xamarinFormsApp = new MvxFormsApp();

           var presenter = new MvxFormsIosShellPagePresenter(Window, xamarinFormsApp);
           Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);
           return presenter;
        }
    }
}
