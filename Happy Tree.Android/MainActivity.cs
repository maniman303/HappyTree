using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: Dependency(typeof(Happy_Tree.Droid.MainActivity))]
namespace Happy_Tree.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IStatusBarColor
    {
        public static Context context;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            context = this;
            LoadApplication(new App());
        }
        public void changestatuscolor(string color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var c = MainActivity.context as FormsAppCompatActivity;
                c?.RunOnUiThread(() => c.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color)));
            }

        }
        public bool GetHasHardwareKeys()
        {
            return ViewConfiguration.Get(Android.App.Application.Context).HasPermanentMenuKey;
        }
        public void fullscreen(bool b)
        {
            var c = MainActivity.context as FormsAppCompatActivity;
            if (b)
            {
                
                c?.RunOnUiThread(() =>
                {
                    c.Window.AddFlags(WindowManagerFlags.Fullscreen);
                    c.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                                             (SystemUiFlags.LowProfile
                                             | SystemUiFlags.Fullscreen
                                             | SystemUiFlags.HideNavigation
                                             | SystemUiFlags.Immersive
                                             | SystemUiFlags.ImmersiveSticky);
                });
            }   
            else
                c?.RunOnUiThread(() =>
                {
                    c.Window.ClearFlags(WindowManagerFlags.Fullscreen);
                    c.Window.ClearFlags(WindowManagerFlags.TranslucentNavigation);
                });
        }
        protected override void OnResume()
        {
            context = this;
            base.OnResume();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}