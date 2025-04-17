using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EnvilopeChako.Common
{
    public class SimulationUtils
    {
        /// <summary>
        /// Simulates a randomized network latency between minMs and maxMs.
        /// </summary>
        public static UniTask SimulateNetworkLatency(int minMs = 100, int maxMs = 1000, DelayType delayType = DelayType.UnscaledDeltaTime)
        {
            int ms = Random.Range(minMs, maxMs);
            return UniTask.Delay(ms, delayType);
        }
    }
}