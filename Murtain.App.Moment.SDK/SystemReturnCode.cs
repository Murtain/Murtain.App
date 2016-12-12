using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.SDK
{
    public enum SystemReturnCode
    {
         InvalidAccountOrPassword = 10001,
         AccountNotExsist,
         ExpiredPassword,
         AccountLocked,
         AccountNotActived,
    }
}
