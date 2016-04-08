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
                address = value;
                ImgUrl = AddressToGravatar.GetImgUrl(address);
                RaisePropertyChanged(() => Address);
            }
        }

        private string imgUrl;
        public string ImgUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; RaisePropertyChanged(() => ImgUrl); }
        }

        public string BalanceSummary
        {
            get { return balanceSummary; }
            set
            {
                balanceSummary = value;
              
                RaisePropertyChanged(() => BalanceSummary);
            }
        }

        private string balanceSummary;
    }
}