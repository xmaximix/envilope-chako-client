using Cysharp.Threading.Tasks;

namespace EnvilopeChako.Modules.Authentication.Domain
{
    public interface IAuthRepository
    {
        UniTask<Result> Login(string email, string password);
        UniTask<Result> Register(string nickname, string email, string password);
        UniTask<Result> Verify(string code);
    }
}