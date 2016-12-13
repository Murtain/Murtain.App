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
using Murtain.App.Moment.Cross.Interactions;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Acr.UserDialogs;

namespace Murtain.App.Moment.Droid.Interactions
{
    public class PopupMenuInteraction : IPopupMenuInteraction
    {
        protected Activity CurrentActivity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }
        public void LoginFogotPasswordLinkPopupMenuShow(Action forgotPassword, Action mobileLogin)
        {
            Application.SynchronizationContext.Post(ignored => {
                Mvx.Resolve<IUserDialogs>().ActionSheet(new ActionSheetConfig
                {
                    Options = new List<ActionSheetOption>() {
                        new ActionSheetOption("�һ�����",forgotPassword)
                    },
                    Cancel = new ActionSheetOption("ȡ��"),
                    Title = "��������",
                    Message = "��������",
                    UseBottomSheet = true
                });
            }, null);
        }
    }
}