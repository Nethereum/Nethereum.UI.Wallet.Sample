using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;
using ReactiveUI;

namespace Nethereum.UI.Core.ViewModels
{
    public class AccountTokenViewModel: BaseViewModel
    {
        private string symbol;

        public string Symbol
        {
            get { return symbol; }
            set { this.RaiseAndSetIfChanged(ref symbol, value); }
        }

        public decimal Balance
        {
            get { return balance; }
            set { this.RaiseAndSetIfChanged(ref balance, value); }
        }

        public string TokenName
        {
            get { return tokenName; }
            set
            {
                this.RaiseAndSetIfChanged(ref tokenName, value);

            }
        }

        public string TokenImgUrl
        {
            get { return tokenImgUrl; }
            set
            {
                this.RaiseAndSetIfChanged(ref tokenImgUrl, value);
            }
        }

        private decimal balance;

        private string tokenName;

        private string tokenImgUrl;

       
    }
}