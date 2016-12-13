using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Murtain.App.Moment.Cross.ViewModels
{
    public class ValidateMobilePhoneViewModel : MvxViewModel
    {
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;

                ButtonEnabled = !string.IsNullOrEmpty(_mobile);
                RaisePropertyChanged(() => ButtonEnabled);
                RaisePropertyChanged(() => Mobile);
            }
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set
            {
                _buttonEnabled = value;
                RaisePropertyChanged(() => ButtonEnabled);
            }
        }
        public ICommand ValidateMobilePhoneCommand
        {
            get
            {
                return new MvxCommand(() => { ShowViewModel<ValidateMessageCaptchaViewModel>(new { mobile = this.Mobile }); });
            }
        }
    }
}
