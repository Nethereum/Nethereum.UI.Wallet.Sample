using ReactiveUI;

namespace Nethereum.UI.Core.ViewModels
{
    public class AccountSummaryViewModel : BaseViewModel
    {
        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                this.RaiseAndSetIfChanged(ref address, value);
                ImgUrl = AddressToGravatar.GetImgUrl(address);
            }
        }

        private string imgUrl;
        public string ImgUrl
        {
            get { return imgUrl; }
            set { this.RaiseAndSetIfChanged(ref imgUrl, value); }
        }

        public string BalanceSummary
        {
            get { return balanceSummary; }
            set { this.RaiseAndSetIfChanged(ref balanceSummary, value); }
        }

        private string balanceSummary;
    }
}