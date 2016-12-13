using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.Cross.ViewModels
{
    public class DescoverViewModel : MvxViewModel
    {
        public DescoverViewModel()
        {
            this.Datas = new List<string>();
        }
        public List<string> Datas { get; set; }

    }
}
