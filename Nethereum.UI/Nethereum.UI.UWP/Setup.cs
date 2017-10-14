using System;
using System.Diagnostics;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using Xamarin.Forms;
using XamlControls = Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using MvvmCross.Forms.Presenters;
using Nethereum.Wallet.Services;
using Nethereum.UI.UWP.Services;
using MvvmCross.Forms.Core;
using MvvmCross.Uwp.Platform;
using MvvmCross.Uwp.Views;

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

            var xamarinFormsApp = new MvxFormsApplication();
            var presenter = new MvxFormsWindowsUWPShellPagePresenter(rootFrame, xamarinFormsApp);
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);


            return presenter;
        }
    }
}