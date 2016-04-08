using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Wallet.Model;

namespace Nethereum.Wallet.Services
{
    public class TokenRegistryService : ITokenRegistryService
    {

        public List<ContractToken> GetDefaultContractTokens()
        {
            return new List<ContractToken>(new[] { new ContractToken() { Address = "0xffb1c99b389ba527a9194b1606b3565a07da3eef", Name = "Maker", NumberOfDecimalPlaces = 18, Symbol = "MKR", ImgUrl="makerIcon.png" } });
            
        }

        public List<ContractToken> GetRegisteredTokens()
        {
            return GetDefaultContractTokens();
        }

        public Task RegisterToken(ContractToken token)
        {
            throw new NotImplementedException();
        }
    }
}