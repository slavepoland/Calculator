using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ACalculator;
using Xamarin.Forms;
using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android;
using System;
using Android.Content;
using ACalculator.Function;

[assembly: ExportRenderer(typeof(NoKeyboardEntry), typeof(ACalculator.Droid.NoKeyboardEntryRenderer))]
namespace ACalculator.Droid
{
    [Activity(Label = "Calculator", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, 
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int WidthInDp { get; set; }
        public static int HeightInDp { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Funkcje funkcje = new Funkcje();
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            using (var metrics = Resources.DisplayMetrics)
            {
                WidthInDp = ConvertPixelsToDp(metrics.WidthPixels);
                HeightInDp = ConvertPixelsToDp(metrics.HeightPixels);
            }
            Global.WidthInDp = WidthInDp; 
            Global.HeightInDp = HeightInDp;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
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
                funkcje.ChangeFontAndPadding("landscape");
                //funkcje.FontAndPaddingConverterPage("landscape");
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                funkcje.ChangeFontAndPadding("portrait");
                //funkcje.FontAndPaddingConverterPage("portrait");
            }
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)(pixelValue / Resources.DisplayMetrics.Density);
            return dp;
        }

    }

    public class NoKeyboardEntryRenderer : EntryRenderer
    {

        public NoKeyboardEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnFocusChangeRequested(object sender, VisualElement.FocusRequestArgs e)
        {
            e.Result = true;

            if (e.Focus)
                this.Control.RequestFocus();
            else
                this.Control.ClearFocus();

            // Disable the Keyboard on Focus
            Control.ShowSoftInputOnFocus = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                ((NoKeyboardEntry)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((NoKeyboardEntry)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }
            // Disable the Keyboard on Focus
            Control.ShowSoftInputOnFocus = false;
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                try
                {
                    // Disable the Keyboard on Focus
                    Control.ShowSoftInputOnFocus = false;

                    // incase if the focus was moved from another Entry
                    // Forcefully dismiss the Keyboard 
                    InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(Control.WindowToken, 0);
                }
                catch (Exception ex)
                {
                    _ = ex;
                }
            }
        }
    }
}