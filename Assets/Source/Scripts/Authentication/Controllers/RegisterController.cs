using System;
using R3;
using VContainer.Unity;

namespace EnvilopeChako.Authentication
{
    public class RegisterController : IDisposable
    {
        private readonly IRegisterView registerView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        // Рекативный поток, который сигнализирует об успешной инициации регистрации
        private readonly Subject<Unit> registrationInitiatedSubject = new Subject<Unit>();

        public Observable<Unit> OnRegistrationInitiated => registrationInitiatedSubject;

        public RegisterController(IRegisterView registerView, IAuthService authService)
        {
            this.registerView = registerView;
            this.authService = authService;
        }

        public void Initialize()
        {
            registerView.OnRegisterSubmitClicked
                .Subscribe(_ => HandleRegister())
                .AddTo(disposables);
        }

        private async void HandleRegister()
        {
            registerView.ShowMessage("Processing registration...");
            bool result = await authService.RegisterAsync(
                registerView.NicknameInput,
                registerView.EmailInput,
                registerView.PasswordInput);
            
            if (registerView.EmailInput == "") return;
            
            if (result)
            {
                registerView.ShowMessage("Registration initiated. Please check your email for the verification code.");
                registrationInitiatedSubject.OnNext(Unit.Default);
            }
            else
            {
                registerView.ShowMessage("Registration failed. Please try again.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
            registrationInitiatedSubject.OnCompleted();
        }
    }
}