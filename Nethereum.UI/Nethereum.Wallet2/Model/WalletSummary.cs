using System.Collections.Generic;
using System.Linq;

namespace Nethereum.Wallet.Model
{
    public class WalletSummary
    {
        public List<AccountInfo> AccountsInfo { get; private set; }
        public List<AccountToken> TokenBalanceSummary { get; private set; }

        public EthAccountToken EthBalanceSummary { get; private set; }

        public WalletSummary(List<AccountInfo> accountsInfo)
        {
            this.AccountsInfo = accountsInfo;
            InitialiseEthBalanceSummary();
            InitialiseTokenBalanceSummary();
        }

        private void InitialiseEthBalanceSummary()
        {
            EthBalanceSummary = new EthAccountToken {Balance = AccountsInfo.Sum(x => x.Eth.Balance)};
        }

        private void InitialiseTokenBalanceSummary()
        {
            TokenBalanceSummary = new List<AccountToken>();
            var tokens = AccountsInfo.SelectMany(x => x.AccountTokens);
            var symbols = tokens.Select(x => x.Symbol).Distinct();

            foreach(var symbol in symbols)
            {
                var tokenSummary = new AccountToken() { Symbol = symbol };
                tokenSummary.Balance = tokens.Where(x => x.Symbol == symbol).Sum(x => x.Balance);
                TokenBalanceSummary.Add(tokenSummary);
            }
        }


    }
}