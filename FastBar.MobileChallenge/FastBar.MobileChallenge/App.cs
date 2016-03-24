using FastBar.MobileChallenge.Services;
using FastBar.MobileChallenge.ViewModels;
using FastBar.MobileChallenge.Views;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace FastBar.MobileChallenge
{
    public class App : Application
    {
        public App()
        {
            RegisterViews();

            var page = Resolver.Resolve<IApiService>().IsAuthenticated
                ? ViewFactory.CreatePage<EventsViewModel, EventsView>()
                : ViewFactory.CreatePage<LoginViewModel, LoginView>();
            MainPage = new NavigationPage((Page)page);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        private void RegisterViews()
        {
            ViewFactory.Register<LoginView, LoginViewModel>();
            ViewFactory.Register<EventsView, EventsViewModel>();
        }
    }
}