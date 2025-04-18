using UnityEngine;

namespace EnvilopeChako.Common
{
    [CreateAssetMenu(menuName = "Config/FeatureFlags")]
    public class FeatureFlags : ScriptableObject
    {
        [Header("Authentication")]
        public bool UseNewLoginFlow = false;
    }

    public interface IFeatureToggleService
    {
        bool IsEnabled(string key);
    }

    public class ScriptableFeatureToggle : IFeatureToggleService
    {
        private readonly FeatureFlags _flags;

        public ScriptableFeatureToggle(FeatureFlags flags) => _flags = flags;

        public bool IsEnabled(string key)
        {
            switch (key)
            {
                case "NewLoginFlow": return _flags.UseNewLoginFlow;
                default: return false;
            }
        }
    }
}