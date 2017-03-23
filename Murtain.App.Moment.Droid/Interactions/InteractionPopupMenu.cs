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
using Com.Bigkoo.Alertview;
using static Android.Views.View;
using Java.Lang;
using Android.Views.InputMethods;

namespace Murtain.App.Moment.Droid.Interactions
{
    public class InteractionPopupMenu : IInteractionPopupMenu
    {

        private readonly Activity _currentActivity;

        private readonly InputMethodManager _inputMethodManager;

        public InteractionPopupMenu()
        {
            _currentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }

        public void LoginFogotPasswordLink(Action register, Action forgotPassword, Action mobileLogin)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                new AlertView("忘记密码"
                                , null
                                , "取消"
                                , new string[] { "找回密码" }
                                , new string[] { "立即注册", "使用手机号登录" }
                                , _currentActivity
                                , AlertView.Style.ActionSheet
                                , new LoginFogotPasswordLinkAlertViewItemClickListener(register,forgotPassword,mobileLogin))
                            .Show();

            }, null);
        }

        public class LoginFogotPasswordLinkAlertViewItemClickListener : Java.Lang.Object, IOnItemClickListener, IOnDismissListener
        {
            private readonly Action _forgotPassword;
            private readonly Action _mobileLogin;
            private readonly Action _register;

            public LoginFogotPasswordLinkAlertViewItemClickListener(Action register, Action forgotPassword, Action mobileLogin)
            {
                _register = register;
                _forgotPassword = forgotPassword;
                _mobileLogin = mobileLogin;
            }

            public void OnDismiss(Java.Lang.Object view)
            {
                ((AlertView)view).Dismiss();
            }

            public void OnItemClick(Java.Lang.Object view, int index)
            {
                Application.SynchronizationContext.Post(ignored =>
                {
                    switch (index)
                    {
                        case 0:
                            _register();
                            break;
                        case 1:
                            _forgotPassword();
                            break;
                        case 2:
                            _mobileLogin();
                            break;
                        default:
                            break;
                    }
                }, null);
            }
        }
    }


}