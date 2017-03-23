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
using MvvmCross.Droid.Platform;
using Murtain.App.Moment.Cross;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Murtain.App.Moment.Cross.Services;
using Murtain.App.Moment.Droid.Interactions;
using Murtain.App.Moment.Cross.Interactions;

namespace Murtain.App.Moment.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }


        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterType<IInteractionToast>(() => new InteractionToast());
            Mvx.RegisterType<IInteractionPopupMenu>(() => new InteractionPopupMenu());

            return new StartupApp();
        }
    }
}