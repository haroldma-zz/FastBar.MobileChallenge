using Android.App;
using Android.Preferences;
using Autofac;
using FastBar.MobileChallenge.Droid.Utilities;
using FastBar.MobileChallenge.Utilities;

namespace FastBar.MobileChallenge.Droid.Modules
{
    internal class UtilityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SettingsUtility(PreferenceManager.GetDefaultSharedPreferences(Application.Context))).As<ISettingsUtility>();
            builder.RegisterType<CredentialUtility>().As<ICredentialUtility>();
        }
    }
}