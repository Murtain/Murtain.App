using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Services
{
    public interface ILoginViewService : IApplicationService
    {
        Task LoginAsync(string username, string password);
    }
}
