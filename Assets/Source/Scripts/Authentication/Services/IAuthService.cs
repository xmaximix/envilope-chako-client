using Cysharp.Threading.Tasks;

namespace EnvilopeChako.Authentication
{
    public interface IAuthService 
    {
        UniTask<bool> RegisterAsync(string nickname, string email, string password);
        UniTask<bool> LoginAsync(string email, string password);
        UniTask<bool> VerifyCodeAsync(string code);
    }
}