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
using Acr.UserDialogs;

namespace Murtain.App.Moment.Droid.Interactions
{
    public class InteractionToast : IInteractionToast
    {
        public void Show(string message)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                Mvx.Resolve<IUserDialogs>().InfoToast(message);

            }, null);

            Toast.MakeText(Application.Context.ApplicationContext, message, ToastLength.Long)
                 .Show();
        }
    }
}