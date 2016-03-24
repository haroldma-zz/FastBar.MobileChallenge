using System;
using System.IO;
using Autofac;
using FastBar.MobileChallenge.iOS.Modules;
using FastBar.MobileChallenge.Modules;
using Foundation;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;
using XLabs.Platform.Device;

namespace FastBar.MobileChallenge.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

            if (!Resolver.IsSet)
            {
                SetIoc();
            }

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void SetIoc()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => AppleDevice.CurrentDevice).As<IDevice>();
            builder.RegisterType<SQLitePlatformIOS>().As<ISQLitePlatform>();

            var dbPath = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.LibraryDirectory, NSSearchPathDomain.User)[0].AbsoluteString.Replace("file://", "");
            dbPath = Path.Combine(dbPath, "local.sqlite");
            builder.Register(c => new SQLiteConnection(c.Resolve<ISQLitePlatform>(), dbPath)).AsSelf();

            builder.RegisterModule<UtilityModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ViewModelModule>();

            Resolver.SetResolver(new AutofacResolver(builder.Build()));
        }
    }
}