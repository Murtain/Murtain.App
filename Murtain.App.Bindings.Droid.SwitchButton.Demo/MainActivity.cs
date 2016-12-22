using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CH.Ielse.View;
using Android.Graphics.Drawables;
using System.Threading.Tasks;
using Java.Lang;

namespace Murtain.App.Bindings.Droid.SwitchButton.Demo
{
    [Activity(Label = "Murtain.App.Bindings.Droid.SwitchButton.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
        , SwitchView.IOnClickListener
        , SwitchView.IOnStateChangedListener
    {
        int count = 1;
        private ImageView ivLoadingTiny;
        private SwitchView svNotice;

        public void OnClick(View v)
        {
            //throw new NotImplementedException();
        }

        public void ToggleToOff(SwitchView p0)
        {
            this.ivLoadingTiny.Visibility = ViewStates.Visible;
            ((AnimationDrawable)this.ivLoadingTiny.Background).Start();

            new Handler().PostDelayed(() =>
            {
                svNotice.ToggleSwitch(false);

                this.ivLoadingTiny.Visibility = ViewStates.Gone;
                ((AnimationDrawable)this.ivLoadingTiny.Background).Stop();

            }, 3000);

        }

        public void ToggleToOn(SwitchView p0)
        {
            this.ivLoadingTiny.Visibility = ViewStates.Visible;
            ((AnimationDrawable)this.ivLoadingTiny.Background).Start();

            new Handler().PostDelayed(() =>
            {
                svNotice.ToggleSwitch(true);

                this.ivLoadingTiny.Visibility = ViewStates.Gone;
                ((AnimationDrawable)this.ivLoadingTiny.Background).Stop();
            }, 3000);


        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            this.ivLoadingTiny = FindViewById<ImageView>(Resource.Id.LoadingTiny);
            this.svNotice = FindViewById<SwitchView>(Resource.Id.SwitchView);

            this.svNotice.SetOnStateChangedListener(this);
            this.svNotice.SetOnClickListener(this);

        }
    }
}

