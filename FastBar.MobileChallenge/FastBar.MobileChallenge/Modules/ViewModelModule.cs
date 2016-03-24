using Autofac;
using FastBar.MobileChallenge.ViewModels;

namespace FastBar.MobileChallenge.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<EventsViewModel>();
        }
    }
}