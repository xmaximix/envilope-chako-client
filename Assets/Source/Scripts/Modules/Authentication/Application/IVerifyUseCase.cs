using Cysharp.Threading.Tasks;
using EnvilopeChako.Modules.Authentication.Domain;

namespace EnvilopeChako.Modules.Authentication.Application
{
    public interface IVerifyUseCase
    {
        UniTask<Result> Execute(string code);
    }
}