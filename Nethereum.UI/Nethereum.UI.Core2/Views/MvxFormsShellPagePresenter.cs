using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Nethereum.UI.Core.Messages;
using Xamarin.Forms;

namespace Nethereum.UI.Core.Views
{
    /// <summary>
    /// Master detail page use to hold a hamburger menu (master) and the pages (detail). 
    /// Menu page view model publish a message on menu selection, the shell page subscribes to the event, resolves the page / view model and sets the detail.
    /// </summary>
    public class MvxFormsShellPagePresenter : MvxFormsPagePresenter
    {
        public MasterDetailPage ShellPage { get; set; }

        private Dictionary<Type, Page> contentPages;

        private NavigationPage currentShellContentNavigationPage;

        private IShellNavigationPageInitialiser shellNavigationPageInitialiser;

        private IMvxMessenger messenger;
        private MvxSubscriptionToken subscriptionToken;


        public MvxFormsShellPagePresenter()
        { }

        public MvxFormsShellPagePresenter(Application mvxFormsApp)
        {
            MvxFormsApp = mvxFormsApp;
        }

        public void InitialiseShell<TShellMenuSelectedMessage>(MasterDetailPage shellPage, Type menuViewModelType, Type initialDetailViewModelType, IShellNavigationPageInitialiser shellNavigationPageInitialiser = null)
            where TShellMenuSelectedMessage : ShellMenuSelectedMessage
        {
            this.ShellPage = shellPage;
          
            this.shellNavigationPageInitialiser = shellNavigationPageInitialiser;
            this.contentPages = new Dictionary<Type, Page>();
            this.ShellPage.Master = ResolvePage(menuViewModelType);

            ShowContent(initialDetailViewModelType);

            messenger = Mvx.Resolve<IMvxMessenger>();
            subscriptionToken = messenger.SubscribeOnMainThread<TShellMenuSelectedMessage>(MenuSelected);
        }

        private void MenuSelected(ShellMenuSelectedMessage shellMenuSelected)
        {
            ShowContent(shellMenuSelected.SelectedMenuItem.PageViewModelType);
        }

        public override void Show(MvxViewModelRequest request)
        {
            if (TryShowPage(request))
                return;
                
            Mvx.Error("Skipping request for {0}", request.ViewModelType.Name);
        }

        protected virtual void CustomShellPlatformInitialization(Page mainPage)
        {

        }

        private void ShowContent(Type pageViewModelType)
        {
            if (!contentPages.ContainsKey(pageViewModelType))
            {
                var contentPage = new NavigationPage(ResolvePage(pageViewModelType));
                contentPages.Add(pageViewModelType, contentPage);
                if (shellNavigationPageInitialiser != null)
                {
                    shellNavigationPageInitialiser.InitialiseNavigationPage(contentPage);
                }
            }
            
            BeforeSetContentPlatformCustomisation();

            ShellPage.Detail = contentPages[pageViewModelType];

            currentShellContentNavigationPage = (NavigationPage)ShellPage.Detail;
            
            AfterSettingContentPlatformCustomisation();
           
        }

        protected virtual void BeforeSetContentPlatformCustomisation()
        {
            
        }

        protected virtual void AfterSettingContentPlatformCustomisation()
        {
            if (Device.Idiom != TargetIdiom.Tablet)
                ShellPage.IsPresented = false;
        }
            
        private  bool TryShowPage(MvxViewModelRequest request)
        {
            var page = ResolvePage(request);
            if (page == null) return false;

            
            //TODO: Check if main page is Shell if not initialise as NavigationPage.
            
            var mainPage = MvxFormsApp.MainPage;// as NavigationPage;

            if (mainPage == null) 
            {
                MvxFormsApp.MainPage = page;
                //mainPage = MvxFormsApp.MainPage as NavigationPage;
                CustomShellPlatformInitialization(page);
            }
            else
            {
                try
                {
                    if (currentShellContentNavigationPage != null) currentShellContentNavigationPage.PushAsync(page);
                    // else
                    // calling this sync blocks UI and never navigates hence code continues regardless here
                    // mainPage.PushAsync(page);
                }
                catch (Exception e)
                {
                    Mvx.Error("Exception pushing {0}: {1}\n{2}", page.GetType(), e.Message, e.StackTrace);
                    return false;
                }
            }

            return true;
        }

        protected Page ResolvePage(Type pageViewModelType)
        {
            var request = new MvxViewModelRequest(pageViewModelType, null, null, null);
            return ResolvePage(request);
        }

        protected Page ResolvePage(MvxViewModelRequest request)
        {
            var page = MvxPresenterHelpers.CreatePage(request);
            if (page == null) return null;

            var viewModel = MvxPresenterHelpers.LoadViewModel(request);

            page.BindingContext = viewModel;
            return page;
        }
    }
}