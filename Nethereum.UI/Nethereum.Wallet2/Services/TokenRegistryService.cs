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
            return new List<ContractToken>(
                new[]
                {
                    new ContractToken() { Address = "0xc66ea802717bfb9833400264dd12c2bceaa34a6d", Name = "Maker", NumberOfDecimalPlaces = 18, Symbol = "MKR", ImgUrl="makerIcon.png" },
                   // new ContractToken() { Address = "0xe0b7927c4af23765cb51314a0e0521a9645f0e2a", Name = "Digix", NumberOfDecimalPlaces = 18, Symbol = "DGD", ImgUrl="digixIcon.png" },
                   // new ContractToken() { Address = "0xAf30D2a7E90d7DC361c8C4585e9BB7D2F6f15bc7", Name = "First Blood", NumberOfDecimalPlaces = 18, Symbol = "FB", ImgUrl="firstbloodIcon.png" },
                    //new ContractToken() { Address = "0xa74476443119A942dE498590Fe1f2454d7D4aC0d", Name = "Golem", NumberOfDecimalPlaces = 18, Symbol = "GOL", ImgUrl="golemIcon.png" },
                    new ContractToken() { Address = "0x48c80F1f4D53D5951e5D5438B54Cba84f29F32a5", Name = "Augur", NumberOfDecimalPlaces = 18, Symbol = "REP", ImgUrl="augurIcon.png" },
                    //new ContractToken() { Address = "0xaec2e87e0a235266d9c5adc9deb4b2e29b54d009", Name = "Singular DTV", NumberOfDecimalPlaces = 18, Symbol = "SNGLS", ImgUrl="singularDTVIcon.png" }
                });
            
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