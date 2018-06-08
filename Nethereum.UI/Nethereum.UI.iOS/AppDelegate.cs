using System;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using MvvmCross.Forms.Platforms.Ios.Core;
using Nethereum.UI.Core;
using UIKit;
using Xamarin.Forms;

namespace Nethereum.UI.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, FormsApp>
    {
    }
}


