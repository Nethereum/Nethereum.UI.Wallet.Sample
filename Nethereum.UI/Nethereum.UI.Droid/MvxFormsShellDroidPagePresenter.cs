using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Nethereum.UI.Core.Views;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Presenter.Core;

namespace Nethereum.UI.Droid
{
 
        public class MvxFormsShellDroidPagePresenter
            : MvxFormsShellPagePresenter
            , IMvxAndroidViewPresenter
        {
            public MvxFormsShellDroidPagePresenter()
            {
            }

            public MvxFormsShellDroidPagePresenter(MvxFormsApp mvxFormsApp)
                : base(mvxFormsApp)
            {
            }
        }
    
}