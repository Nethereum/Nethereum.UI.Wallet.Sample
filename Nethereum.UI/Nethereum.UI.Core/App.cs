using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using Xamarin.Forms;
using MvvmCross.Platform;
using System.Reflection;
using Nethereum.UI.Core.Views;

namespace Nethereum.UI.Core
{

    //app start navigates to a navigationservice
    //this checks if we have any settings if not shows the page to input the url
    //this also loads specific configuration, ie.. am i desktop uwp etc?
    //Root is a MasterDetailPage
    //Root needs to be injected IMenuService.. which gets all the menu items
    //Root needs to also resolve MenuPage
    public class App : MvxApplication
    {
        public static bool IsWindows10 { get; set; }
        public override void Initialize()
        {
           

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            typeof(Nethereum.Wallet.Services.IEthWalletService).GetTypeInfo().Assembly.CreatableTypes()
           .EndingWith("Service")
           .AsInterfaces()
           .RegisterAsLazySingleton();

           

            // Construct custom application start object
            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            

            // request a reference to the constructed appstart object 
            var appStart = Mvx.Resolve<IMvxAppStart>();

            // register the appstart object
            RegisterAppStart(appStart);

         
            //if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            //{
            //    Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();
            //}
          
        }
    }
}
