using EnvilopeChako.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnvilopeChako.Authentication
{
    public class VerificationView : MonoBehaviour, IVerificationView
    {
        [Header("Элементы UI для верификации")]
        [SerializeField] private TMP_InputField verificationCodeInputField;
        [SerializeField] private Button verificationSubmitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        public Observable<Unit> OnVerificationSubmitClicked =>
            verificationSubmitButton.OnClickAsObservable();

        public string VerificationCodeInput => verificationCodeInputField.text;

        public void ShowMessage(string message)
        {
            if (messageText != null)
            {
                messageText.text = message;
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}