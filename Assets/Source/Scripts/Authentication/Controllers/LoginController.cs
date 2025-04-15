using System;
using R3;

namespace EnvilopeChako.Authentication
{
    public class LoginController : IDisposable
    {
        private readonly ILoginView loginView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new();

        // Вместо Action создаём реактивный поток для успешного логина
        private readonly Subject<Unit> loginSuccessSubject = new();
        // Аналог для запроса открытия регистрации
        private readonly Subject<Unit> registerOpenSubject = new();

        public Observable<Unit> OnLoginSuccess => loginSuccessSubject;
        public Observable<Unit> OnRegisterRequested => registerOpenSubject;

        public LoginController(ILoginView loginView, IAuthService authService)
        {
            this.loginView = loginView;
            this.authService = authService;
        }

        public void Initialize()
        {
            // Подписываемся на поток кликов на кнопку логина
            loginView.OnLoginSubmitClicked
                .Subscribe(_ => HandleLogin())
                .AddTo(disposables);

            // Подписываемся на событие открытия регистрации
            loginView.OnRegisterClicked
                .Subscribe(_ => registerOpenSubject.OnNext(Unit.Default))
                .AddTo(disposables);
        }

        private async void HandleLogin()
        {
            loginView.ShowMessage("Processing login...");
            bool result = await authService.LoginAsync(loginView.EmailInput, loginView.PasswordInput);
            if (result)
            {
                loginView.ShowMessage("Login successful!");
                loginSuccessSubject.OnNext(Unit.Default);
            }
            else
            {
                loginView.ShowMessage("Login failed. Please try again.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
            loginSuccessSubject.OnCompleted();
            registerOpenSubject.OnCompleted();
        }
    }
}