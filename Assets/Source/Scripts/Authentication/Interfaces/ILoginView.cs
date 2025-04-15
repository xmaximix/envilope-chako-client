
using R3;

namespace EnvilopeChako.Authentication
{
    public interface ILoginView
    {
        Observable<Unit> OnLoginSubmitClicked { get; }
        Observable<Unit> OnRegisterClicked { get; }
        string EmailInput { get; }
        string PasswordInput { get; }
        void ShowMessage(string message);
        void SetActive(bool isActive);
    }
}