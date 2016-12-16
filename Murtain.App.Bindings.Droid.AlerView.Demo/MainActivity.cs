using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Bigkoo.Alertview;
using Java.Lang;
using Android.Views.InputMethods;
using static Android.Views.View;

namespace Murtain.App.Bindings.Droid.AlerView.Demo
{
    [Activity(Label = "Murtain.App.Bindings.Droid.AlerView.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnItemClickListener, IOnDismissListener, IOnFocusChangeListener
    {
        int count = 1;
        private AlertView alertView;
        private AlertView alertViewExt;
        private EditText alertViewEditText;
        private InputMethodManager inputMethodManager;

        public void OnDismiss(Java.Lang.Object p0)
        {
            this.alertView.Dismiss();
        }

        public void OnItemClick(Java.Lang.Object p0, int p1)
        {
            Toast.MakeText(this, $"itme click {p1}", ToastLength.Short);
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


            inputMethodManager = (InputMethodManager)GetSystemService(Context.InputMethodService);


            FindViewById<Button>(Resource.Id.btnNarmal).Click += (sender, agrs) =>
            {
                this.alertView = new AlertView("标题", "内容", "取消", new string[] { "确定" }, null, this, AlertView.Style.Alert, this);
                this.alertView.Show();
            };

            FindViewById<Button>(Resource.Id.btnMessageTip).Click += (sender, agrs) =>
            {
                this.alertView = new AlertView("标题", "内容", null, new string[] { "确定" }, null, this, AlertView.Style.Alert, this);
                this.alertView.Show();
            };
            FindViewById<Button>(Resource.Id.btnActionSheet).Click += (sender, agrs) =>
            {
                this.alertView = new AlertView("标题", null, "取消", new string[] { "高亮按钮1" }, new string[] { "其他按钮1", "其他按钮2", "其他按钮3" }, this, AlertView.Style.ActionSheet, this);
                this.alertView.Show();
            };
            FindViewById<Button>(Resource.Id.btnActionSheetSelection).Click += (sender, agrs) =>
            {
                this.alertView = new AlertView("上传头像", null, "取消", null, new string[] { "拍照", "从相册中选择" }, this, AlertView.Style.ActionSheet, this);
                this.alertView.Show();
            };
            FindViewById<Button>(Resource.Id.btnActionSheetTip).Click += (sender, agrs) =>
            {
                this.alertView = new AlertView("标题", "内容", "取消", null, null, this, AlertView.Style.ActionSheet, this)
                         .SetCancelable(true);
                this.alertView.Show();
            };
            FindViewById<Button>(Resource.Id.btnExtend).Click += (sender, agrs) =>
            {

                this.alertView = new AlertView("标题", "内容", "取消", new string[] { "确定" }, null, this, AlertView.Style.Alert, this)
                                        .SetCancelable(true)
                                        .SetOnDismissListener(this);

                this.alertViewExt = new AlertView("提示", "请完善你的个人资料！", "取消", null, new string[] { "完成" }, this, AlertView.Style.Alert, this);
                ViewGroup extView = (ViewGroup)LayoutInflater.From(this).Inflate(Resource.Layout.AlertViewEditText, null);
                this.alertViewEditText = (EditText)extView.FindViewById(Resource.Id.AlertViewEditText);

                this.alertViewEditText.OnFocusChangeListener = this;

                this.alertViewExt.AddExtView(extView);

                alertViewExt.Show();

            };
        }
        public void OnFocusChange(View v, bool hasFocus)
        {
            this.alertViewExt.SetMarginBottom(inputMethodManager.IsActive && hasFocus ? 120 : 0);

            Toast.MakeText(this, $"InpuntMethodManager {inputMethodManager.IsActive}", ToastLength.Short).Show();
        }
        private void closeKeyboard()
        {
            inputMethodManager.HideSoftInputFromWindow(alertViewEditText.WindowToken, 0);
            this.alertViewExt.SetMarginBottom(0);
        }
    }
}

