using Murtain.App.Moment.SDK.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.SDK.Client
{
    public class MomentClient : IMomentClient
    {
        public Task Request()
        {
            try
            {

            }
            catch (WebException)
            {
            }

            return Task.FromResult(0);
        }


        public Task RequestAsync<T>(T input) where T : class, IRequestModel
        {
            return Task.FromResult(0);
        }
    }
}
