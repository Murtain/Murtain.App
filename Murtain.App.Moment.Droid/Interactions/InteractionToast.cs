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
using Murtain.App.Moment.Cross.Services;
using MvvmCross.Platform;

namespace Murtain.App.Moment.Droid.Interactions
{
    public class InteractionToast : IInteractionToast
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context.ApplicationContext, message, ToastLength.Long)
                 .Show();
        }
    }
}