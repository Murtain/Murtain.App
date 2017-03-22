using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.Interactions
{
    public interface IInteractionPopupMenu : IApplicationService
    {
        void LoginFogotPasswordLink(Action register,Action forgotPassword, Action mobileLogin);
    }
}
