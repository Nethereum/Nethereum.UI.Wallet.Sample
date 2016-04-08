using MvvmCross.Plugins.Messenger;
using Nethereum.UI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.UI.Core.Messages
{
    public class ShellMenuSelectedMessage : MvxMessage
    {
        public ShellMenuItem SelectedMenuItem { get; private set; }
        public ShellMenuSelectedMessage(object sender, ShellMenuItem selectedMenuItem) : base(sender)
        {
            this.SelectedMenuItem = selectedMenuItem;
        }
    }
}
