using System;
using R3;

namespace EnvilopeChako.Authentication
{
    public class VerificationController : IDisposable
    {
        private readonly IVerificationView verificationView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new();

        // Событие, сигнализирующее, что верификация прошла успешно
        public event Action OnVerificationSuccess;

        public VerificationController(IVerificationView verificationView, IAuthService authService)
        {
            this.verificationView = verificationView;
            this.authService = authService;
        }

        public void Initialize()
        {
            // Подписываемся на событие отправки кода верификации
            verificationView.OnVerificationSubmitClicked
                .Subscribe(_ => HandleVerification())
                .AddTo(disposables);
        }

        private async void HandleVerification()
        {
            verificationView.ShowMessage("Верификация кода...");
            bool result = await authService.VerifyCodeAsync(verificationView.VerificationCodeInput);
            if (result)
            {
                verificationView.ShowMessage("Верификация успешна! Регистрация завершена.");
                OnVerificationSuccess?.Invoke();
            }
            else
            {
                verificationView.ShowMessage("Неверный код. Попробуйте еще раз.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}