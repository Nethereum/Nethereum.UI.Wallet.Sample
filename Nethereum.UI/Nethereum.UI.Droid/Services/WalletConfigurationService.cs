using Nethereum.Wallet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace Nethereum.UI.Droid.Services
{
    //public class WalletConfigurationService : IWalletConfigurationService
    //{

    //    public WalletConfigurationService()
    //    {
    //        this.ClientUrl = "https://mainnet.infura.io/";
    //        this.Client = new RpcClient(new Uri(ClientUrl));
    //    }

    //    private IClient client;
    //    public IClient Client
    //    {
    //        get
    //        {
    //            return client;
    //        }

    //        set
    //        {
    //            client = value;
    //        }
    //    }

    //    public string ClientUrl { get; set; }

    //    public bool IsConfigured()
    //    {
    //        //Todo get info from storage
    //        return true;
    //    }

    //    public string[] GetAccounts()
    //    {
    //        return new[]
    //        {
    //            "0x7bb0b08587b8a6b8945e09f1baca426558b0f06a", //MKR
    //            //"0xd5c64535f370fe00c5c73b8a42e4943dff4806b7", //SNGLS
    //            //"0xab11204cfeaccffa63c2d23aef2ea9accdb0a0d5", //AUG
    //            //"0x4319c142f7b6cd722fc3a49289b8a22a7a51ca1e", //GOLEM
    //            //"0x42da8a05cb7ed9a43572b5ba1b8f82a0a6e263dc", //First blood
    //            //"0x4366ddc115d8cf213c564da36e64c8ebaa30cdbd", //Digix
    //            "0xb794f5ea0ba39494ce839613fffba74279579268" //POLO cold wallet
    //        };
    //    }
    //}
}
