// Features/Authentication/Presentation/Views/LoginView.cs

using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;
using EnvilopeChako.Common;
using EnvilopeChako.Extensions;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;
using EnvilopeChako.Services;

namespace EnvilopeChako.Modules.Authentication.Presentation.Views
{
    public class LoginView : ReactiveView<LoginViewModel>
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        [Header("Addressable & Localization Keys")]
        [SerializeField] private string addressableKey;
        [SerializeField] private string successKey = "login_success";

        private AddressableHandle<LoginView> _handle;
        private IStringTable _strings;

        public async UniTask InitAsync(
            LoginViewModel vm,
            IStringTable strings,
            string successKeyOverride,
            string addressableKeyOverride,
            CancellationToken ct)
        {
            _strings = strings;
            successKey = successKeyOverride;
            addressableKey = addressableKeyOverride;

            _handle = await AddressablesLoader
                .LoadAsync<LoginView>(addressableKey)
                .AttachExternalCancellation(ct);

            // Bind VM now
            Init(vm);
        }

        protected override void Bind()
        {
            // Field bindings
            AddSubscription(
                emailField
                    .OnValueChangedAsObservable(destroyCancellationToken)
                    .Subscribe(x => ViewModel.Email.Value = x)
            );
            AddSubscription(
                passwordField
                    .OnValueChangedAsObservable(destroyCancellationToken)
                    .Subscribe(x => ViewModel.Password.Value = x)
            );

            // Enable/disable submit
            AddSubscription(
                ViewModel.CanSubmit
                    .Subscribe(ok => submitButton.interactable = ok)
            );

            // Click → SubmitAsync
            AddSubscription(
                submitButton
                    .OnClickAsObservable(destroyCancellationToken)
                    .SubscribeAwait(async (_, ct) => await ViewModel.SubmitAsync(ct),
                        AwaitOperation.Drop)
            );

            // Success → localized message
            AddSubscription(
                ViewModel.OnSuccess
                    .Subscribe(_ =>
                    {
                        messageText.text = _strings.Get(successKey);
                    })
            );
        }

        protected override void OnDestroy()
        {
            _handle.Release();
            base.OnDestroy();
        }
    }
}