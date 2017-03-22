using Murtain.App.Moment.Cross;
using Murtain.App.Moment.Cross.Services;
using Murtain.App.Moment.Cross.ViewModels;
using Murtain.App.Moment.SDK.Client;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross
{
    public class StartupApp : MvxApplication
    {
        public StartupApp()
        {
            CreatableTypes()
                .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && t != typeof(IApplicationService))
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterType<IRestClient, DefaultRestClient>();

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
        }
    }
}
