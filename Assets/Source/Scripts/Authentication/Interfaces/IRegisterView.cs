using R3;

namespace EnvilopeChako.Authentication
{
    public interface IRegisterView
    {
        Observable<Unit> OnRegisterSubmitClicked { get; }
        string NicknameInput { get; }
        string EmailInput { get; }
        string PasswordInput { get; }
        void ShowMessage(string message);
        void SetActive(bool isActive);
    }
}