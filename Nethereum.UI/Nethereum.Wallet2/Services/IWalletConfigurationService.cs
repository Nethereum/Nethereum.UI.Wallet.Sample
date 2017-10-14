using Nethereum.JsonRpc.Client;

namespace Nethereum.Wallet.Services
{
    public interface IWalletConfigurationService
    {
        string ClientUrl { get; set; }

        IClient Client { get; set; }

        bool IsConfigured();

        string[] GetAccounts();
    }
}