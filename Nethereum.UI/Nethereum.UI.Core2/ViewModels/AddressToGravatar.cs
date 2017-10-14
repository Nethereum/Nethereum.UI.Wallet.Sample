namespace Nethereum.UI.Core.ViewModels
{
    public static class AddressToGravatar
    {
        public static string GetImgUrl(string address)
        {
            var hash = address;
            if (address.StartsWith("0x")) hash = address.Substring(2);
            return "http://www.gravatar.com/avatar/" + hash + ".jpg?s=75&d=retro";
        }
    }
}