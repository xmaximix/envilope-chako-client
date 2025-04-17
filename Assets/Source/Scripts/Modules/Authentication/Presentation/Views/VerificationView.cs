using EnvilopeChako.Extensions;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;

namespace EnvilopeChako.Modules.Authentication.Presentation.Views
{
    // Features/Authentication/Presentation/Views/VerificationView.cs
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Common;
using Services;
using System.Threading;

namespace Features.Authentication.Presentation.Views
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
        private IStringTable                         _strings;

        public async UniTask InitAsync(
            VerifyViewModel vm,
            IStringTable strings,
            string successKeyOverride,
            string addressableKeyOverride,
            CancellationToken ct)
        {
            _strings       = strings;
            successKey     = successKeyOverride;
            addressableKey = addressableKeyOverride;

            _handle = await AddressablesLoader
                .LoadAsync<VerificationView>(addressableKey)
                .AttachExternalCancellation(ct);

            Init(vm);
        }

        protected override void Bind()
        {
            AddSubscription(
                codeField.OnValueChangedAsObservable(this.destroyCancellationToken)
                         .Subscribe(x => ViewModel.Code.Value = x)
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
}