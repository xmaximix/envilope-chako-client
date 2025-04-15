using EnvilopeChako.Common;
using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.DI.Scopes
{
    public class MenuLifetimeScope : BaseLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterEntryPoint<MenuEntryPoint>();
        }
    }
}