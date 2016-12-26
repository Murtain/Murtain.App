using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Widget.SeekBar;
using System;
using Android.Graphics;
using Android.Renderscripts;
using System.Threading.Tasks;

namespace Murtain.App.Droid.BlurView.Demo
{
    [Activity(Label = "BlurView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnSeekBarChangeListener
    {
        private SeekBar seekBar;
        private BlurViewLayout blurViewLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            this.seekBar = (SeekBar)FindViewById(Resource.Id.MainSeekBar);
            this.blurViewLayout = (BlurViewLayout)FindViewById(Resource.Id.MainBlurView);
            this.blurViewLayout.SetBlurImage(GetDrawable(Resource.Drawable.dayu));
            this.blurViewLayout.ShowBlurView();
            this.seekBar.SetOnSeekBarChangeListener(this);
            this.seekBar.Max = 100;
            //seekBar.StopTrackingTouch += BlurImageHandler;

        }
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            this.blurViewLayout.SetBlurLevel(progress);
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
            //throw new NotImplementedException();
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            //throw new NotImplementedException();
        }
    }
}

