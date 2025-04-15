using System;
using R3;
using UnityEngine;

namespace EnvilopeChako.Authentication
{
    public class VerificationController : IDisposable
    {
        private readonly IVerificationView verificationView;
        private readonly IAuthService authService;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        // Рекативный поток для уведомления об успешной верификации
        private readonly Subject<Unit> verificationSuccessSubject = new Subject<Unit>();

        public Observable<Unit> OnVerificationSuccess => verificationSuccessSubject;

        public VerificationController(IVerificationView verificationView, IAuthService authService)
        {
            this.verificationView = verificationView;
            this.authService = authService;
        }

        public void Initialize()
        {
            verificationView.OnVerificationSubmitClicked
                .Subscribe(_ => HandleVerification())
                .AddTo(disposables);
        }

        private async void HandleVerification()
        {
            verificationView.ShowMessage("Verifying code...");
            bool result = await authService.VerifyCodeAsync(verificationView.VerificationCodeInput);
            if (result)
            {
                verificationView.ShowMessage("Verification successful! Registration complete.");
                verificationSuccessSubject.OnNext(Unit.Default);
            }
            else
            {
                verificationView.ShowMessage("Invalid code. Please try again.");
            }
        }

        public void Dispose()
        {
            disposables.Dispose();
            verificationSuccessSubject.OnCompleted();
        }
    }
}