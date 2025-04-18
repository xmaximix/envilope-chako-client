using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public interface ILoginUseCase
    {
        UniTask<Result> Execute(string email, string password);
    }
}