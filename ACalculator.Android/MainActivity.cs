using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace ACalculator.Droid
{
    [Activity(Label = "ACalculator", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize |
        ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int WidthInDp { get; set; }
        public static int HeightInDp { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            using (var metrics = Resources.DisplayMetrics)
            {
                WidthInDp = ConvertPixelsToDp(metrics.WidthPixels);
                HeightInDp = ConvertPixelsToDp(metrics.HeightPixels);
            }

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(WidthInDp, HeightInDp));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            Funkcje funkcje = new Funkcje();
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                funkcje.ChangeFontAndPadding("landscape", WidthInDp, HeightInDp);
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                funkcje.ChangeFontAndPadding("portrait", WidthInDp, HeightInDp);
            }
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

    }
}