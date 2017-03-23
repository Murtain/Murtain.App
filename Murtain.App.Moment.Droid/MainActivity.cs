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
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform;

namespace Murtain.App.Moment.Droid
{
    [Activity(Label = "MainActivity",MainLauncher = true)]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            SetContentView(Resource.Layout.Main);
        }
    }
}