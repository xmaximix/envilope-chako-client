using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.DI
{
    public abstract class BaseLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            SetupInstallers(builder);
        }

        private void SetupInstallers(IContainerBuilder builder)
        {
            var installers = GetComponentsInChildren<IInstaller>(true);
            foreach (var installer in installers) installer.Install(builder);
        }
    }
}