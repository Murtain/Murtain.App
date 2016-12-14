using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.ViewModels
{
    public class SwipeMenuListViewModel : MvxViewModel
    {
        public SwipeMenuListViewModel()
        {
            this.Datas = new List<object>();
        }

        public List<object> Datas { get; set; }
    }
}
