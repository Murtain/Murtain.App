using Murtain.App.Moment.SDK.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Services
{
    public class LoginViewService : ILoginViewService
    {
        private readonly IRestClient _client;
        private readonly IInteractionToast _interactionToast;

        public LoginViewService(IRestClient client, IInteractionToast interactionToast)
        {
            _client = client;
            _interactionToast = interactionToast;
        }

        public Task LoginAsync(string username, string password)
        {
            _interactionToast.Show("无效的账号或密码");
            return Task.FromResult(0);
        }
    }
}
