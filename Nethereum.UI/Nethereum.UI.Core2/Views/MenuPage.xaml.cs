using Nethereum.UI.Core.ViewModels;
using Nethereum.UI.Core.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Nethereum.UI.Core.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            
            InitializeComponent();
            if (!App.IsWindows10)
            {
                BackgroundColor = Color.FromHex("#03A9F4");
                ListViewMenu.BackgroundColor = Color.FromHex("#F5F5F5");
            }
        }
    }

}

