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
            /*
             * 'morden': {
+    'ETH': '0xfbc7f6b58daa9f99816b6cc77d2a7f4b327fa7bc',
+    'DAI': '0xa6581e37bb19afddd5c11f1d4e5fb16b359eb9fc',
+    'MKR': '0xffb1c99b389ba527a9194b1606b3565a07da3eef',
+    'DGD': '0x3c6f5633b30aa3817fa50b17e5bd30fb49bddd95'
+  } */
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