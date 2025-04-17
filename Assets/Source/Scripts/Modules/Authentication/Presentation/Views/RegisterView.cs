using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using EnvilopeChako.Extensions;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;
using EnvilopeChako.Services;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnvilopeChako.Modules.Authentication.Presentation.Views
{
    public class RegisterView : ReactiveView<RegisterViewModel>
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField nicknameField;
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        [Header("Addressable & Localization Keys")]
        [SerializeField] private string addressableKey;
        [SerializeField] private string initiatedKey = "registration_initiated";

        private AddressableHandle<RegisterView> _handle;
        private IStringTable _strings;

        public async UniTask InitAsync(
            RegisterViewModel vm,
            IStringTable strings,
            string initiatedKeyOverride,
            string addressableKeyOverride,
            CancellationToken ct)
        {
            _strings = strings;
            initiatedKey = initiatedKeyOverride;
            addressableKey = addressableKeyOverride;

            _handle = await AddressablesLoader
                .LoadAsync<RegisterView>(addressableKey)
                .AttachExternalCancellation(ct);

            Init(vm);
        }

        protected override void Bind()
        {
            AddSubscription(
                nicknameField.OnValueChangedAsObservable(this.destroyCancellationToken)
                    .Subscribe(x => ViewModel.Nickname.Value = x)
            );
            AddSubscription(
                emailField.OnValueChangedAsObservable(this.destroyCancellationToken)
                    .Subscribe(x => ViewModel.Email.Value = x)
            );
            AddSubscription(
                passwordField.OnValueChangedAsObservable(this.destroyCancellationToken)
                    .Subscribe(x => ViewModel.Password.Value = x)
            );
            AddSubscription(
                ViewModel.CanSubmit.Subscribe(ok => submitButton.interactable = ok)
            );
            AddSubscription(
                submitButton.OnClickAsObservable(this.destroyCancellationToken)
                    .SubscribeAwait(async (_, ct) => await ViewModel.SubmitAsync(ct),
                        AwaitOperation.Drop)
            );
            AddSubscription(
                ViewModel.OnInitiated
                    .Subscribe(_ => { messageText.text = _strings.Get(initiatedKey); })
            );
        }

        protected override void OnDestroy()
        {
            _handle.Release();
            base.OnDestroy();
        }
    }
}