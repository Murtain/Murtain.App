using Murtain.App.Moment.Cross.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Murtain.App.Moment.Cross.ViewModels
{
    public class ValidateMessageCaptchaViewModel : MvxViewModel
    {
        private readonly IToastInteraction toastInteraction;

        public ValidateMessageCaptchaViewModel(IToastInteraction toastInteraction)
        {
            this.toastInteraction = toastInteraction;
        }

        public void Init(string mobile)
        {
            this.Mobile = mobile;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

        }
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; RaisePropertyChanged(() => Mobile); }
        }

        private string _messageCaptcha;
        public string MessageCaptcha
        {
            get { return _messageCaptcha; }
            set { _messageCaptcha = value; ButtonEnabled = !string.IsNullOrEmpty(_messageCaptcha); RaisePropertyChanged(() => ButtonEnabled); RaisePropertyChanged(() => MessageCaptcha); }
        }


        public string Tips
        {
            get { return "我们已经向您的手机：" + this.Mobile + "发送了一条验证码，请输入您的验证码"; }
        }

        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { _buttonEnabled = value; RaisePropertyChanged(() => ButtonEnabled); }
        }


        public ICommand VlidateMessageCaptchaCommand
        {
            get
            {
                return new MvxCommand(() => { toastInteraction.Show("短信验证成功"); ShowViewModel<LoginViewModel>(); });
            }
        }

        public ICommand ResendMessageCaptchaCommand
        {
            get
            {
                return new MvxCommand(() => { toastInteraction.Show("短信验证码重发成功"); });
            }
        }
    }
}
