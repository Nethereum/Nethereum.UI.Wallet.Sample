using MvvmCross.ViewModels;

namespace Nethereum.UI.Core.ViewModels
{
    public class BaseViewModel<TParam> : MvxViewModel<TParam>, IBaseViewModel
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
                icon = value;
                RaisePropertyChanged(() => Icon);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
    }


    public class BaseViewModel : MvxViewModel, IBaseViewModel
    {
        private string icon;
        private bool isBusy;
        private string title;


        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                RaisePropertyChanged(() => Icon);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
    }
}