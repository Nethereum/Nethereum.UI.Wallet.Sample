using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.OS;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Core.ViewModels;
using Android.App;
using Android.Content.PM;
using ImageCircle.Forms.Plugin.Droid;
using MvvmCross.Forms.Core;

namespace Nethereum.UI.Droid
{
    [Activity(Label = "MvxFormsApplicationActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MvxFormsApplicationActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            ImageCircleRenderer.Init();

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            var mvxFormsApp = new MvxFormsApplication();
            LoadApplication(mvxFormsApp);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsShellDroidPagePresenter;
            presenter.MvxFormsApp = mvxFormsApp;

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();
        }
    }
}