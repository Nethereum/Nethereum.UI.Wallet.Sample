using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Nethereum.UI.Core.Model;
using Nethereum.UI.Core.Services;
using Xamarin.Forms;

namespace Nethereum.UI.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IHomeMenuService homeMenuService;
        private readonly IMvxNavigationService navigationService;

        private List<ShellMenuItem> menuItems;
        private readonly IMvxMessenger messenger;

        private ShellMenuItem selectedMenuItem;
    

        public MenuViewModel(IMvxMessenger messenger, IHomeMenuService homeMenuService, IMvxNavigationService navigationService)
        {
            this.messenger = messenger;
            this.homeMenuService = homeMenuService;
            this.navigationService = navigationService;
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
                if (selectedMenuItem != value)
                {
                    selectedMenuItem = value;
                    RaisePropertyChanged(() => SelectedMenuItem);

                    NavigateToSelectedMenuCommand.Execute(null);
                }
            }
        }

       

        public ICommand NavigateToSelectedMenuCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                    {
                        var vmType = SelectedMenuItem.PageViewModelType;
                        await navigationService.Navigate(vmType);

                    },
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