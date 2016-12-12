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

namespace Murtain.App.Moment.Droid.Interactions
{
    public class PopupMenuInteraction : IPopupMenuInteraction
    {

        public void ShowPopupMenu(string param)
        {

            switch (param)
            {
                case "POPUP_MENU_LOGIN_FORGOT_PASSWORD":
                    LoginFogotPasswordLinkPopupMenuShow();
                    break;
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        private void LoginFogotPasswordLinkPopupMenuShow()
        {

            var inflater = LayoutInflater.From(Application.Context.ApplicationContext);
            var view = inflater.Inflate(Resource.Layout.Login, null);

            var sender = view.FindViewById<TextView>(Resource.Id.LoginForgotPasswordLink);
            var menu = new PopupMenu(Application.Context.ApplicationContext, sender);
            menu.Inflate(Resource.Layout.LoginForgotPasswordPopupMenu);
            menu.Show();
        }
    }
}