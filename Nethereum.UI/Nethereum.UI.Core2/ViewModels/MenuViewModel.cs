using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Nethereum.UI.Core.Messages;
using Nethereum.UI.Core.Model;
using Nethereum.UI.Core.Services;

namespace Nethereum.UI.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IHomeMenuService homeMenuService;

        private List<ShellMenuItem> menuItems;
        private readonly IMvxMessenger messenger;

        private ShellMenuItem selectedMenuItem;

        public MenuViewModel(IMvxMessenger messenger, IHomeMenuService homeMenuService)
        {
            this.messenger = messenger;
            this.homeMenuService = homeMenuService;
           
        }

        public List<ShellMenuItem> MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }

        public ShellMenuItem SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                RaisePropertyChanged(() => SelectedMenuItem);

                NavigateToSelectedMenuCommand.Execute(null);
            }
        }


        public ICommand NavigateToSelectedMenuCommand
        {
            get
            {
                return new MvxCommand(() => { messenger.Publish(new ShellMenuSelectedMessage(this, SelectedMenuItem)); },
                    () => SelectedMenuItem != null);
            }
        }

        public override void Start()
        {
            base.Start();
            Title = "Nethereum Wallet";
            Icon = "slideout.png";
            MenuItems = homeMenuService.GetMenuItems();
            SelectedMenuItem = MenuItems[0];
        }
    }
}