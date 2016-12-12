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
    [Activity(Label = "LoginView")]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            SetContentView(Resource.Layout.Login);

            RegisterForContextMenu(LayoutInflater.Inflate(Resource.Layout.Login,null));
        }


        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu.Add(0, 1, Menu.None, "找回密码");
            menu.Add(0, 2, Menu.None, "短信验证登录");
        }
    }
}