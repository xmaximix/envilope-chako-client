using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace EnvilopeChako.Services
{
    public interface IConfigService
    {
        UniTask<Dictionary<string, string>> FetchAsync(CancellationToken ct);
    }
}