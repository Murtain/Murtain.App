using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Bigkoo.Svprogresshud;

namespace Murtain.App.Bindings.Droid.SVProgressHUD.Demo
{
    [Activity(Label = "SVProgressHUD", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, Com.Bigkoo.Svprogresshud.Listener.IOnDismissListener
        , ISVProgressHUDProgressActicity
    {
        int count = 1;
        private int progress;
        private SVProgressHUDProgressHandler progressHandler;
        private Com.Bigkoo.Svprogresshud.SVProgressHUD SVProgressHUD;

        public void OnDismiss(Com.Bigkoo.Svprogresshud.SVProgressHUD p0)
        {
            Toast.MakeText(this, "dismiss", ToastLength.Short).Show();
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

            this.SVProgressHUD = new Com.Bigkoo.Svprogresshud.SVProgressHUD(this);
            this.SVProgressHUD.OnDismissListener = this;

            this.progressHandler = new SVProgressHUDProgressHandler(this.SVProgressHUD);
        }
        [Java.Interop.Export("ShowLoading")]
        public void ShowLoading(View view)
        {
            this.SVProgressHUD.Show();
        }
        [Java.Interop.Export("ShowMask")]
        public void ShowMask(View view)
        {
            this.SVProgressHUD.ShowWithMaskType(Com.Bigkoo.Svprogresshud.SVProgressHUD.SVProgressHUDMaskType.Black);
        }
        [Java.Interop.Export("ShowMaskWithText")]
        public void ShowMaskWithText(View view)
        {
            this.SVProgressHUD.ShowWithStatus("交易处理中...");
        }

        [Java.Interop.Export("ShowProgress")]
        public void ShowProgress(View view)
        {
            progress = 0;
            this.SVProgressHUD.ProgressBar.Progress = progress;
            this.SVProgressHUD.ShowWithProgress("加载中...", Com.Bigkoo.Svprogresshud.SVProgressHUD.SVProgressHUDMaskType.Black);

            this.progressHandler.SendEmptyMessage(0);
        }
        [Java.Interop.Export("ShowInfo")]
        public void ShowInfo(View view)
        {
            this.SVProgressHUD.ShowInfoWithStatus("订单已提交，请耐心等待");
        }
        [Java.Interop.Export("ShowSuccess")]
        public void ShowSuccess(View view)
        {
            this.SVProgressHUD.ShowSuccessWithStatus("提交审核成功");
        }
        [Java.Interop.Export("ShowError")]
        public void ShowError(View view)
        {
            this.SVProgressHUD.ShowErrorWithStatus("网络连接失败");
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.RepeatCount == 0)
            {
                if (this.SVProgressHUD.IsShowing)
                {
                    this.SVProgressHUD.Dismiss();
                    return false;
                }
            }

            return base.OnKeyDown(keyCode, e);
        }

        public Com.Bigkoo.Svprogresshud.SVProgressHUD GetSVProgressHUD()
        {
            return this.SVProgressHUD;
        }

    }
    public class SVProgressHUDProgressHandler : Handler
    {
        public int Progress { get; set; }
        public Com.Bigkoo.Svprogresshud.SVProgressHUD SVProgressHUD { get; set; }

        public SVProgressHUDProgressHandler(Com.Bigkoo.Svprogresshud.SVProgressHUD SVProgressHUD)
        {
            this.Progress = 0;
            this.SVProgressHUD = SVProgressHUD;
        }
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);

            this.Progress++;


            if (this.SVProgressHUD.ProgressBar.Max != this.SVProgressHUD.ProgressBar.Progress)
            {
                this.SVProgressHUD.ProgressBar.Progress = this.Progress;
                this.SendEmptyMessageDelayed(0, 10);
            }
            else
            {
                this.Progress = 0;
                if (this.SVProgressHUD.IsShowing)
                {
                    this.SVProgressHUD.Dismiss();
                }
            }
        }
    }

    public interface ISVProgressHUDProgressActicity
    {
        Com.Bigkoo.Svprogresshud.SVProgressHUD GetSVProgressHUD();
    }
}

