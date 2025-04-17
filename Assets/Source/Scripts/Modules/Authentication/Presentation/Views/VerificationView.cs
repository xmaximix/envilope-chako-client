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
    public class VerificationView : ReactiveView<VerifyViewModel>
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField codeField;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        [Header("Addressable & Localization Keys")]
        [SerializeField] private string addressableKey;
        [SerializeField] private string successKey = "verification_success";

        private AddressableHandle<VerificationView> _handle;
        private IStringTable _strings;

        public async UniTask InitAsync(
            VerifyViewModel vm,
            IStringTable strings,
            string successKeyOverride,
            string addressableKeyOverride,
            CancellationToken ct)
        {
            _strings = strings;
            successKey = successKeyOverride;
            addressableKey = addressableKeyOverride;

            _handle = await AddressablesLoader
                .LoadAsync<VerificationView>(addressableKey)
                .AttachExternalCancellation(ct);

            Init(vm);
        }

        protected override void Bind()
        {
            AddSubscription(
                codeField.OnValueChangedAsObservable(destroyCancellationToken)
                    .Subscribe(x => ViewModel.Code.Value = x)
            );
            AddSubscription(
                ViewModel.CanSubmit.Subscribe(ok => submitButton.interactable = ok)
            );
            AddSubscription(
                submitButton.OnClickAsObservable(destroyCancellationToken)
                    .SubscribeAwait(async (_, ct) => await ViewModel.SubmitAsync(ct),
                        AwaitOperation.Drop)
            );
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