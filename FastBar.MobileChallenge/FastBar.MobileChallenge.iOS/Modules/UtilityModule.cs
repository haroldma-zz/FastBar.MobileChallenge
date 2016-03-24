using Autofac;
using FastBar.MobileChallenge.iOS.Utilities;
using FastBar.MobileChallenge.Utilities;

namespace FastBar.MobileChallenge.iOS.Modules
{
    internal class UtilityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsUtility>().As<ISettingsUtility>();
            builder.Register(c => new CredentialUtility("FastBar")).As<ICredentialUtility>();
        }
    }
}