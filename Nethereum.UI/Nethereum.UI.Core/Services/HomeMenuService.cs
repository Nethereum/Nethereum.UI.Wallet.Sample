using System.Collections.Generic;
using Nethereum.UI.Core.Model;
using Nethereum.UI.Core.ViewModels;

namespace Nethereum.UI.Core.Services
{
    public class HomeMenuService : IHomeMenuService
    {
        public List<ShellMenuItem> GetMenuItems()
        {
            return new List<ShellMenuItem>
            {
                new ShellMenuItem {Title = "Balance", PageViewModelType = typeof (BalanceSummaryViewModel), Icon = "ethIcon.png"},
                new ShellMenuItem {Title = "Accounts", PageViewModelType = typeof (AccountsSummaryViewModel), Icon = "blog.png"},
                new ShellMenuItem {Title = "Add Token", PageViewModelType = typeof (TokenEntryViewModel), Icon = "ethIcon.png"},
                new ShellMenuItem {Title = "About", PageViewModelType = typeof (AboutViewModel), Icon = "about.png"}
               
            };
        }
    }
}