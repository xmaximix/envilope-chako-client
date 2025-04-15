using System;
using UnityEngine;
using VContainer.Unity;

namespace EnvilopeChako.Authentication
{
    public class AuthFlowManager : IInitializable, IDisposable
    {
        private readonly ILoginView loginView;
        private readonly IRegisterView registerView;
        private readonly IVerificationView verificationView;

        private readonly LoginController loginController;
        private readonly RegisterController registerController;
        private readonly VerificationController verificationController;

        public AuthFlowManager(
            ILoginView loginView,
            IRegisterView registerView,
            IVerificationView verificationView,
            LoginController loginController,
            RegisterController registerController,
            VerificationController verificationController)
        {
            this.loginView = loginView;
            this.registerView = registerView;
            this.verificationView = verificationView;
            this.loginController = loginController;
            this.registerController = registerController;
            this.verificationController = verificationController;
        }

        public void Initialize()
        {
            loginView.SetActive(true);
            registerView.SetActive(false);
            verificationView.SetActive(false);
            
            registerController.Initialize();
            verificationController.Initialize();
            loginController.Initialize();

            registerController.OnRegistrationInitiated += ShowVerificationScreen;
            verificationController.OnVerificationSuccess += OnVerificationSuccess;
            loginController.OnLoginSuccess += OnLoginSuccess;
        }

        private void ShowVerificationScreen()
        {
            registerView.SetActive(false);
            verificationView.SetActive(true);
        }

        private void OnVerificationSuccess()
        {
            verificationView.SetActive(false);
            Debug.Log("Registration and verification completed successfully.");
        }

        private void OnLoginSuccess()
        {
            loginView.SetActive(false);
            Debug.Log("Login completed successfully.");
        }

        public void Dispose()
        {
        }
    }
}