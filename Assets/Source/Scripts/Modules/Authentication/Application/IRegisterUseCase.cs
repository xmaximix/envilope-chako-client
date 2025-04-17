using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public interface IRegisterUseCase
    {
        UniTask<Result> Execute(string nickname, string email, string password);
    }
}