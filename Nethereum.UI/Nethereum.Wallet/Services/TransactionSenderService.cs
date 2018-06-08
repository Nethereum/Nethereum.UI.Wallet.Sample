using System;
using System.Threading.Tasks;
using Nethereum.Contracts.CQS;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;

namespace Nethereum.Wallet.Services
{
    public class TransactionSenderService:ITransactionSenderService
    {
        private readonly IWalletConfigurationService _walletConfigurationService;
        private readonly IAccountKeySecureStorageService _accountKeySecureStorageService;

        public TransactionSenderService(IWalletConfigurationService walletConfigurationService,
            IAccountKeySecureStorageService accountKeySecureStorageService)
        {
            _walletConfigurationService = walletConfigurationService;
            _accountKeySecureStorageService = accountKeySecureStorageService;
        }

        public Task<string> SendTransactionAsync<TFunctionMessage>(TFunctionMessage functionMessage, string contractAddress) where TFunctionMessage: ContractMessage
        {
            var web3 = GetWeb3(functionMessage.FromAddress);
            return web3.Eth.GetContractHandler(contractAddress).SendRequestAsync(functionMessage);
        }

        public Task<string> SendTransactionAsync(TransactionInput transactionInput, string contractAddres)
        {
            var web3 = GetWeb3(transactionInput.From);
            return web3.TransactionManager.SendTransactionAsync(transactionInput);
        }

        private Web3.Web3 GetWeb3(string accountAddress)
        {
            var privateKey = _accountKeySecureStorageService.GetPrivateKey(accountAddress);
            if(privateKey == null) throw new Exception("Account not configured for signing transactions");
            //todo chainId
            return new Web3.Web3(new Account(privateKey), _walletConfigurationService.ClientUrl);
        }
    }
}