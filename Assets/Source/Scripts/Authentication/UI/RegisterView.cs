using EnvilopeChako.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnvilopeChako.Authentication
{
    public class RegisterView : MonoBehaviour, IRegisterView
    {
        [Header("Элементы UI для регистрации")]
        [SerializeField] private TMP_InputField nicknameInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button registerSubmitButton;
        [SerializeField] private TextMeshProUGUI messageText;

        // Преобразуем событие нажатия кнопки в R3 Observable<Unit>
        public Observable<Unit> OnRegisterSubmitClicked =>
            registerSubmitButton.OnClickAsObservable();

        // Получение значений из input-полей
        public string NicknameInput => nicknameInputField.text;
        public string EmailInput => emailInputField.text;
        public string PasswordInput => passwordInputField.text;

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