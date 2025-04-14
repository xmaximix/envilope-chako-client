using System;
using R3;
using VContainer.Unity;

namespace EnvilopeChako.Authentication
{
    public class RegisterController : IDisposable
    {
        private readonly IRegisterView registerView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new();

        // Событие для уведомления, что регистрация инициирована и нужно показать экран верификации
        public event Action OnRegistrationInitiated;

        public RegisterController(IRegisterView registerView, IAuthService authService)
        {
            this.registerView = registerView;
            this.authService = authService;
        }

        public void Initialize()
        {
            // Подписываемся на событие отправки регистрации
            registerView.OnRegisterSubmitClicked
                .Subscribe(_ => HandleRegister())
                .AddTo(disposables);
        }

        private async void HandleRegister()
        {
            registerView.ShowMessage("Обработка регистрации...");
            
            // Вызов сервиса регистрации (реализация может быть заменена на реальную логику)
            bool result = await authService.RegisterAsync(
                registerView.NicknameInput,
                registerView.EmailInput,
                registerView.PasswordInput
            );
            
            if (result)
            {
                registerView.ShowMessage("Регистрация инициирована. Проверьте почту для получения кода верификации.");
                OnRegistrationInitiated?.Invoke();
            }
            else
            {
                registerView.ShowMessage("Ошибка регистрации. Попробуйте ещё раз.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}