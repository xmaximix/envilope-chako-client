using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace EnvilopeChako.Services
{
    public class HttpConfigService : IConfigService
    {
        private readonly string _url;

        public HttpConfigService(string url)
        {
            _url = url;
        }

        public async UniTask<Dictionary<string, string>> FetchAsync(CancellationToken ct)
        {
#if UNITY_EDITOR
            await SimulationUtils.SimulateNetworkLatency();
#endif
            var req = UnityWebRequest.Get(_url);
            await req.SendWebRequest().WithCancellation(ct);

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Config fetch failed: " + req.error);
                return new Dictionary<string, string>();
            }

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(req.downloadHandler.text)
                   ?? new Dictionary<string, string>();
        }
    }
}