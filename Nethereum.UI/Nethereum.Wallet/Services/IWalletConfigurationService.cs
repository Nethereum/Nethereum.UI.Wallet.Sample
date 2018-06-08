using Nethereum.JsonRpc.Client;

namespace Nethereum.Wallet.Services
{
    public interface IWalletConfigurationService
    {
        string ClientUrl { get; set; }

        bool IsConfigured();
        Web3.Web3 GetReadOnlyWeb3();
    }
}