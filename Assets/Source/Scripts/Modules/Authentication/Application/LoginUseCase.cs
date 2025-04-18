using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IAuthRepository _repo;
        public LoginUseCase(IAuthRepository repo) => _repo = repo;

        public UniTask<Result> Execute(string email, string password)
        {
            return _repo.Login(email, password);
        }
    }
}