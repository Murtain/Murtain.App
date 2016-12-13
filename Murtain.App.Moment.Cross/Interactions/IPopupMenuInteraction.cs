using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Interactions
{
    public interface IPopupMenuInteraction : IApplicationService
    {
        void LoginFogotPasswordLinkPopupMenuShow(Action forgotPassword, Action mobileLogin);
    }
}
