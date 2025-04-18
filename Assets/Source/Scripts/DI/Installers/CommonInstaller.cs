using VContainer;
using EnvilopeChako.Common;
using UnityEngine;
using ILogger = EnvilopeChako.Common.ILogger;

namespace EnvilopeChako.DI.Installers
{
    public class CommonInstaller : BaseInstaller
    {
        [Header("Feature Flags")]
        [SerializeField] private FeatureFlags featureFlags;

        [Header("Localization Table")]
        [SerializeField] private LocalizationTable localizationTable;

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<ILogger, UnityLogger>(Lifetime.Singleton);
            builder.RegisterInstance<FeatureFlags>(featureFlags);
            builder.RegisterInstance<IStringTable>(localizationTable);
            builder.RegisterInstance<IFeatureToggleService>(
                new ScriptableFeatureToggle(featureFlags));
        }
    }
}