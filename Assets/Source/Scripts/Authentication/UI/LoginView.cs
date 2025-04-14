using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Extensions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace EnvilopeChako.Authentication
{
    public class LoginView : MonoBehaviour, ILoginView
    {
        [Header("Login Panel UI Elements")] [SerializeField]
        private TMP_InputField emailInputField;

        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button loginSubmitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        public Observable<Unit> OnLoginSubmitClicked =>
            loginSubmitButton.OnClickAsObservable();

        public string EmailInput => emailInputField.text;
        public string PasswordInput => passwordInputField.text;

        public void ShowMessage(string message)
        {
            if (messageText != null)
                messageText.text = message;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            loginSubmitButton.OnClickAsObservable();
        }

        public async UniTask LoadLogoAsync(string assetKey, Image targetImage)
        {
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(assetKey);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                targetImage.sprite = handle.Result;
            }
        }
    }
}