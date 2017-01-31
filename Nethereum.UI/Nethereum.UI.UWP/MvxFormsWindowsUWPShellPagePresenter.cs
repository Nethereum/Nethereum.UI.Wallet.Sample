using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.WindowsUWP.Views;
using Nethereum.UI.Core.Views;
using MvvmCross.WindowsUWP.Views;
using MvvmCross.Forms.Presenter.Core;
using Xamarin.Forms;

namespace Nethereum.UI.UWP
{

    public class MvxFormsWindowsUWPShellPagePresenter
        : MvxFormsShellPagePresenter
            , IMvxWindowsViewPresenter
    {
        private readonly IMvxWindowsFrame _rootFrame;

        public MvxFormsWindowsUWPShellPagePresenter(IMvxWindowsFrame rootFrame, Application mvxFormsApp)
            : base(mvxFormsApp)
        {
            
            _rootFrame = rootFrame;
        }

        protected override void CustomPlatformInitialization(NavigationPage mainPage)
        {
            _rootFrame.Navigate(mainPage.GetType(), _rootFrame);
        }

        protected override void CustomShellPlatformInitialization(Page mainPage)
        {
            _rootFrame.Navigate(mainPage.GetType(), _rootFrame);
        }

     
        protected override void BeforeSetContentPlatformCustomisation()
        {
            if (ShellPage.Detail != null && Device.OS == TargetPlatform.WinPhone)
            {
                ShellPage.Detail.Navigation.PopToRootAsync().Wait();
            }
        }

        protected override void AfterSettingContentPlatformCustomisation()
        {
            if (!IsUWPDesktopCheck())
                base.AfterSettingContentPlatformCustomisation();
        }

        protected bool IsUWPDesktopCheck()
        {
            return AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
        }
    }


}
