using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IAuthRepository _repo;
        public RegisterUseCase(IAuthRepository repo) => _repo = repo;
        public UniTask<Result> Execute(string nickname, string email, string password)
        {
            return _repo.Register(nickname, email, password);
        }
    }
}