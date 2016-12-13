using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using Murtain.App.Moment.Cross.ViewModels;

namespace Murtain.App.Moment.Droid.Views
{
    [Activity(Label = "��֤��Ϣ��֤��")]
    public class ValidateMessageCaptchaView : MvxActivity<ValidateMessageCaptchaViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.ValidateMessageCaptcha);
        }

    }
}