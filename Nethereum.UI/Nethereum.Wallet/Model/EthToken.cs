namespace Nethereum.Wallet.Model
{
    public class EthToken : Token
    {
        public EthToken()
        {
            this.Name = "Ether";
            this.Symbol = "ETH";
            this.NumberOfDecimalPlaces = 18;
            this.ImgUrl = "ethIcon.png";
        }
    }
}