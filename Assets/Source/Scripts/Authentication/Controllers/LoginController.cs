using System;
using R3;

namespace EnvilopeChako.Authentication
{
    public class LoginController : IDisposable
    {
        private readonly ILoginView loginView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public event Action OnLoginSuccess;

        public LoginController(ILoginView loginView, IAuthService authService)
        {
            this.loginView = loginView;
            this.authService = authService;
        }

        public void Initialize()
        {
            loginView.OnLoginSubmitClicked
                .Subscribe(_ => HandleLogin())
                .AddTo(disposables);
        }

        private async void HandleLogin()
        {
            loginView.ShowMessage("Processing login...");
            bool result = await authService.LoginAsync(loginView.EmailInput, loginView.PasswordInput);
            if (result)
            {
                loginView.ShowMessage("Login successful!");
                OnLoginSuccess?.Invoke();
            }
            else
            {
                loginView.ShowMessage("Login failed. Please try again.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}