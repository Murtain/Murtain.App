using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Murtain.App.Moment.Cross.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public ICommand LoginLinkCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<LoginViewModel>());
            }
        }
    }
}
