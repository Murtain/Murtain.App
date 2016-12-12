using Murtain.App.Moment.SDK.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMomentClient client;
        private readonly IToastInteraction toastService;
        public LoginService(IMomentClient client, IToastInteraction toastService)
        {
            this.client = client;
            this.toastService = toastService;
        }

        public Task LoginAsync(string username, string password)
        {
            toastService.Show("无效的账号或密码");
            return Task.FromResult(0);
        }
    }
}
