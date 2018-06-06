using Android.App;
using Android.Content.PM;
using MvvmCross.Forms.Platforms.Android.Views;
using Nethereum.UI.Core;

namespace Nethereum.UI.Droid
{
    [Activity(
        Label = "Nethereum Wallet"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/MainTheme"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MvxFormsApplicationActivity : MvxFormsAppCompatActivity<Setup, Core.App, FormsApp>
    {
    }
}