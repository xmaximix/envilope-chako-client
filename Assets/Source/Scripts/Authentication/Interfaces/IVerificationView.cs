using R3;

namespace EnvilopeChako.Authentication
{
    public interface IVerificationView
    {
        Observable<Unit> OnVerificationSubmitClicked { get; }
        string VerificationCodeInput { get; }
        void ShowMessage(string message);
        void SetActive(bool isActive);
    }
}