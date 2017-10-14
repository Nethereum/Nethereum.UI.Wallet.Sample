using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms;
using MvvmCross.Forms.Views;
using MvvmCross.Platform;
using Nethereum.UI.Core.Messages;
using Nethereum.UI.Core.ViewModels;
using Org.BouncyCastle.Asn1;
using Xamarin.Forms;

namespace Nethereum.UI.Core.Views
{
    /// <summary>
    /// Master detail page use to hold a hamburger menu (master) and the pages (detail). 
    /// Menu page view model publish a message on menu selection, the shell page subscribes to the event, resolves the page / view model and sets the detail.
    /// </summary>
    public class ShellPage : MasterDetailPage
    {
        public ShellPage()
        {

            var presenter = Mvx.Resolve<IMvxViewPresenter>();
            var mvxFormPresenter = (MvxFormsShellPagePresenter) presenter;
            mvxFormPresenter.InitialiseShell<ShellMenuSelectedMessage>(this, typeof (MenuViewModel),
                typeof (BalanceSummaryViewModel), new ShellNavigationPageInitialiser());

            InvalidateMeasure();

        }

    }
}
