using Android.App;
using Android.Widget;
using Android.OS;
using Com.Bigkoo.ConvenientBanner;

namespace Murtain.App.Bindings.Droid.ConvenientBanner.Demo
{
    [Activity(Label = "ConvenientBanner", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ConvenientBannerView convenientBannerView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

