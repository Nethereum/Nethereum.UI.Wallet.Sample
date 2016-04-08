using Nethereum.Wallet.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.Wallet.Services
{
    public interface ITokenRegistryService
    {
        List<ContractToken> GetRegisteredTokens();
        Task RegisterToken(ContractToken token);
    }
}
