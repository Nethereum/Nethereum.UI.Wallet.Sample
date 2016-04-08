namespace Nethereum.UI.Core.ViewModels
{
    public class AccountTokenViewModel: BaseViewModel
    {
        private string symbol;

        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                RaisePropertyChanged(() => Symbol);
            }
        }

        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                RaisePropertyChanged(() => Balance);
            }
        }

        public string TokenName
        {
            get { return tokenName; }
            set
            {
                tokenName = value;
                RaisePropertyChanged(() => TokenName);
            }
        }

        public string TokenImgUrl
        {
            get { return tokenImgUrl; }
            set
            {
                tokenImgUrl = value;
                RaisePropertyChanged(() => TokenImgUrl);
            }
        }

        private decimal balance;

        private string tokenName;

        private string tokenImgUrl;


    }
}