using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using FastBar.MobileChallenge.Droid.Modules;
using FastBar.MobileChallenge.Modules;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;
using XLabs.Platform.Device;
using Environment = System.Environment;

namespace FastBar.MobileChallenge.Droid
{
    [Activity(Label = "FastBar.MobileChallenge", Theme = "@android:style/Theme.Material.Light", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            if (!Resolver.IsSet)
            {
                SetIoc();
            }

            LoadApplication(new App());
        }

        private void SetIoc()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => AndroidDevice.CurrentDevice).As<IDevice>();
            builder.RegisterType<SQLitePlatformAndroid>().As<ISQLitePlatform>();

            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbPath = Path.Combine(dbPath, "local.sqlite");
            builder.Register(c => new SQLiteConnection(c.Resolve<ISQLitePlatform>(), dbPath)).AsSelf();

            builder.RegisterModule<UtilityModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ViewModelModule>();

            Resolver.SetResolver(new AutofacResolver(builder.Build()));
        }
    }
}