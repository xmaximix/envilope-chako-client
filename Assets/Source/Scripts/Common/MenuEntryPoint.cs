using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Adapters;
using VContainer.Unity;
using EnvilopeChako.Modules.Authentication.Presentation.Views;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;
using EnvilopeChako.Services;
using UnityEngine;

namespace EnvilopeChako.Common
{
    public class MenuEntryPoint : IAsyncStartable, IDisposable
    {
        private readonly LoginViewModel _loginVM;
        private readonly RegisterViewModel _registerVM;
        private readonly VerifyViewModel _verifyVM;
        private readonly IStringTable _strings;
        private readonly AuthCanvas _authCanvas;

        public MenuEntryPoint(
            AuthCanvas authCanvas,
            LoginViewModel loginVM,
            RegisterViewModel registerVM,
            VerifyViewModel verifyVM,
            IStringTable strings)
        {
            _authCanvas = authCanvas;
            _loginVM = loginVM;
            _registerVM = registerVM;
            _verifyVM = verifyVM;
            _strings = strings;
        }

        public async UniTask StartAsync(CancellationToken ct)
        {
            var loginHandle = await AddressablesLoader.LoadAsync<LoginView>("ui_login", _authCanvas.transform)
                .AttachExternalCancellation(ct);
            await loginHandle.Instance.InitAsync(
                _loginVM, _strings, "login_success", "ui_login", ct);

            var registerHandle = await AddressablesLoader.LoadAsync<RegisterView>("ui_register", _authCanvas.transform)
                .AttachExternalCancellation(ct);
            await registerHandle.Instance.InitAsync(
                _registerVM, _strings, "registration_initiated", "ui_register", ct);

            var verifyHandle = await AddressablesLoader.LoadAsync<VerificationView>("ui_verify", _authCanvas.transform)
                .AttachExternalCancellation(ct);
            await verifyHandle.Instance.InitAsync(
                _verifyVM, _strings, "verification_success", "ui_verify", ct);
        }

        public void Dispose()
        {
        }
    }
}