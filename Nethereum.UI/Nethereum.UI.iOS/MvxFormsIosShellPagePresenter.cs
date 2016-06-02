using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.iOS.Views.Presenters;
using Nethereum.UI.Core.Views;
using UIKit;
using Xamarin.Forms;

namespace Nethereum.UI.iOS
{ 

  
        public class MvxFormsIosShellPagePresenter
            : MvxFormsShellPagePresenter
            , IMvxIosViewPresenter
        {
            private readonly UIWindow _window;

            public MvxFormsIosShellPagePresenter(UIWindow window, Xamarin.Forms.Application mvxFormsApp)
                : base(mvxFormsApp)
            {
                _window = window;
            }

            public virtual bool PresentModalViewController(UIViewController controller, bool animated)
            {
                return false;
            }

            public virtual void NativeModalViewControllerDisappearedOnItsOwn()
            {
            }

            protected override void CustomShellPlatformInitialization(Page mainPage)
            {
                _window.RootViewController = mainPage.CreateViewController();
            }

            protected override void CustomPlatformInitialization(NavigationPage mainPage)
            {
                _window.RootViewController = mainPage.CreateViewController();
            }
        }
    }

