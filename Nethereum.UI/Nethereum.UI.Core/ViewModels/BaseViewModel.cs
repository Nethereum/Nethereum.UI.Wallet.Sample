using MvvmCross.ReactiveUI.Interop;
using MvvmCross.ViewModels;

namespace Nethereum.UI.Core.ViewModels
{
    public class BaseViewModel<TParam> : MvxReactiveViewModel<TParam>, IBaseViewModel
    {
        protected TParam Parameter { get; set; }

        public override void Prepare(TParam parameter)
        {
            this.Parameter = parameter;
        }

        private string icon;
        private bool isBusy;
        private string title;


        public string Icon
        {
            get { return icon; }
            set
            {
                this.RaiseAndSetIfChanged(ref icon, value);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                this.RaiseAndSetIfChanged(ref title, value);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                this.RaiseAndSetIfChanged(ref isBusy, value);
            }
        }
    }


    public class BaseViewModel : MvxReactiveViewModel, IBaseViewModel
    {
        private string icon;
        private bool isBusy;
        private string title;


        public string Icon
        {
            get { return icon; }
            set
            {
                this.RaiseAndSetIfChanged(ref icon, value);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                this.RaiseAndSetIfChanged(ref title, value);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                this.RaiseAndSetIfChanged(ref isBusy, value);
            }
        }
    }
}