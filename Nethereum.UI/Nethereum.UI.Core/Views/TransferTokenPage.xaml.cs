using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Nethereum.UI.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nethereum.UI.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]
    public partial class TransferTokenPage : MvxContentPage<TransferTokenViewModel>
    {
        public TransferTokenPage()
        {
            InitializeComponent();
        }

    }
}
