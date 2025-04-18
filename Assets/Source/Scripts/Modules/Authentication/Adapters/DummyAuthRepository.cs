using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using EnvilopeChako.Modules.Authentication.Domain;
using UnityEngine;

namespace EnvilopeChako.Modules.Authentication.Adapters
{
    public class DummyAuthRepository : IAuthRepository
    {
        public async UniTask<Result> Login(string email, string password)
        {
            await SimulationUtils.SimulateNetworkLatency();
            if (Random.value < 0.1f)
                return Result.Fail("Network error", 503, "Login");
            return Result.Ok;
        }

        public async UniTask<Result> Register(string nickname, string email, string password)
        {
            await SimulationUtils.SimulateNetworkLatency();
            return Random.value < 0.1f
                ? Result.Fail("Registration error", 500, "Register")
                : Result.Ok;
        }

        public async UniTask<Result> Verify(string code)
        {
            await SimulationUtils.SimulateNetworkLatency();
            return code == "0000"
                ? Result.Ok
                : Result.Fail("Invalid code", 400, "Verify");
        }
    }
}