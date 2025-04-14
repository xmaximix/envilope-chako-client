using Cysharp.Threading.Tasks;

namespace EnvilopeChako.Authentication
{
    public class DummyAuthService : IAuthService
    {
        public async UniTask<bool> RegisterAsync(string nickname, string email, string password)
        {
            await UniTask.Delay(1000);
            return true;
        }

        public async UniTask<bool> LoginAsync(string email, string password)
        {
            await UniTask.Delay(500);
            return true;
        }

        public async UniTask<bool> VerifyCodeAsync(string code)
        {
            await UniTask.Delay(500);
            return code == "0000";
        }
    }
}