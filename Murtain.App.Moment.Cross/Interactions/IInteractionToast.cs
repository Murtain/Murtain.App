using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Services
{
    /// <summary>
    /// Toast
    /// </summary>
    public interface IInteractionToast : IApplicationService
    {
        void Show(string message);
    }
}
