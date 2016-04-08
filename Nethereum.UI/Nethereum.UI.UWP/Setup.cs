using MvvmCross.Platform;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using Xamarin.Forms;
using XamlControls = Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using MvvmCross.WindowsUWP.Platform;
using MvvmCross.WindowsUWP.Views;
using MvvmCross.Forms.Presenter.WindowsUWP;
using Nethereum.Wallet.Services;
using Nethereum.UI.UWP.Services;

namespace Nethereum.UI.UWP
{
    public class Setup : MvxWindowsSetup
    {
        private readonly LaunchActivatedEventArgs _launchActivatedEventArgs;

        public Setup(XamlControls.Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame)
        {
            _launchActivatedEventArgs = e;
        }
        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            Mvx.RegisterSingleton<IWalletConfigurationService>(new WalletConfigurationService());

        }
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            Forms.Init(_launchActivatedEventArgs);

            var xamarinFormsApp = new MvxFormsApp();
            var presenter = new MvxFormsWindowsUWPShellPagePresenter(rootFrame, xamarinFormsApp);
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);


            return presenter;
        }
    }
}