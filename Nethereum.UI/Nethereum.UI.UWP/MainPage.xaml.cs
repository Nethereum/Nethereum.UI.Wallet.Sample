using System;
using Windows.System.Profile;
using MvvmCross.Forms.Presenters;
using Xamarin.Forms.Platform.UWP;

namespace Nethereum.UI.UWP
{

    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
          
        }
    }
    ///// <summary>
    ///// An empty page that can be used on its own or navigated to within a Frame.
    ///// </summary>
    //public sealed partial class MainPage : WindowsPage
    //{
    //    public MainPage()
    //    {
    //        this.InitializeComponent();
    //        Nethereum.UI.Core.App.IsWindows10 = true;
    //        var start = Mvx.Resolve<IMvxAppStart>();
    //        start.Start();
            

    //        var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsWindowsUWPShellPagePresenter;
 
    //        LoadApplication(presenter.MvxFormsApp);
    //    }
    //}
}
