using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public class VerifyUseCase : IVerifyUseCase
    {
        private readonly IAuthRepository _repo;
        public VerifyUseCase(IAuthRepository repo) => _repo = repo;
        public UniTask<Result> Execute(string code)
        {
            return _repo.Verify(code);
        }
    }
}