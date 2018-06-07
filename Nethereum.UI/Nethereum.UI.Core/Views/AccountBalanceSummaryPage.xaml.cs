using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Nethereum.UI.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nethereum.UI.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = false)]
    public partial class AccountBalanceSummaryPage : MvxContentPage<AccountBalanceSummaryViewModel>
    {
        public AccountBalanceSummaryPage()
        {
            InitializeComponent();
        }

    }
}
