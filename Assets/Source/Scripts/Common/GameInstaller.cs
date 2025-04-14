using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.Common
{
    public class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterEntryPoint<UnifiedEntryPoint>();
        }
    }
}