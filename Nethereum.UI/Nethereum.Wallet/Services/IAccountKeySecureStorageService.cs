namespace Nethereum.Wallet.Services
{
    public interface IAccountKeySecureStorageService
    {
        string GetPrivateKey(string account);
    }
}