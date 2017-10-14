using System.Collections.Generic;
using Nethereum.UI.Core.Model;

namespace Nethereum.UI.Core.Services
{
    public interface IHomeMenuService
    {
        List<ShellMenuItem> GetMenuItems();
    }
}