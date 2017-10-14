using System;

namespace Nethereum.UI.Core.Model
{
    public class ShellMenuItem : BaseModel
    {
        public string Icon { get; set; }

        public Type PageViewModelType { get; set; }
    }
}