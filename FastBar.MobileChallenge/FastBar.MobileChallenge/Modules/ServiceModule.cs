using Autofac;
using FastBar.MobileChallenge.Services;

namespace FastBar.MobileChallenge.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiService>().As<IApiService>().SingleInstance();
            builder.RegisterType<LocalDataService>().As<ILocalDataService>();
        }
    }
}