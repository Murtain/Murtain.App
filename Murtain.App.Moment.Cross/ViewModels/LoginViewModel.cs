using Murtain.App.Moment.Cross.Interactions;
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
    public class LoginViewModel : MvxViewModel
    {
        private readonly IInteractionPopupMenu _interactionPopupMenu;
        private readonly ILoginViewService _viewLoginService;

        public LoginViewModel(ILoginViewService viewLoginService, IInteractionPopupMenu interactionPopupMenu)
        {
            _viewLoginService = viewLoginService;
            _interactionPopupMenu = interactionPopupMenu;

            this.Email = "392327013@qq.com";
            this.Password = "123456";
            this.RememberMe = true;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(() => Email); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { _rememberMe = value; RaisePropertyChanged(() => RememberMe); }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new MvxCommand(() => Login());
            }
        }
        public ICommand ForgotPasswordCommand
        {
            get
            {
                return new MvxCommand<string>(param => ForgotPassword(param));
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new MvxCommand(() => Register());
            }
        }
        private async void Register()
        {
            await Task.FromResult(0);
        }
        private async void ForgotPassword(string param)
        {
            _interactionPopupMenu.LoginFogotPasswordLink(
                () =>{
                    ShowViewModel<ValidateMobilePhoneViewModel>();
                }, () => {
                    ShowViewModel<ValidateMobilePhoneViewModel>();
                },
                () =>{
                    ShowViewModel<ValidateMobilePhoneViewModel>();
                }
            );
            await Task.FromResult(0);
        }

        private async void Login()
        {
            await _viewLoginService.LoginAsync(Email, Password);
        }
    }
}
