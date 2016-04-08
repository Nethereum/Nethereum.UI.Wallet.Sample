using Xamarin.Forms;

namespace Nethereum.UI.Core.Views
{
    public class ShellNavigationPageInitialiser:IShellNavigationPageInitialiser
    {
        public void InitialiseNavigationPage(NavigationPage navigationPage)
        {
            navigationPage.BarBackgroundColor = Color.FromHex("#03A9F4");
            navigationPage.BarTextColor = Color.White;
        }
    }
}