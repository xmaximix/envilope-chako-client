using EnvilopeChako.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.Authentication
{
    public class AuthInstaller : LifetimeScope
    {
        [Header("UI References")]
        [SerializeField] private LoginView loginView;       // Привяжите через инспектор
        [SerializeField] private RegisterView registerView; // Привяжите через инспектор
        [SerializeField] private VerificationView verificationView; // Привяжите через инспектор

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterInstance<ILoginView>(loginView);
            builder.RegisterInstance<IRegisterView>(registerView);
            builder.RegisterInstance<IVerificationView>(verificationView);

            builder.Register<IAuthService, DummyAuthService>(Lifetime.Singleton);

            builder.Register<LoginController>(Lifetime.Scoped);
            builder.Register<RegisterController>(Lifetime.Scoped);
            builder.Register<VerificationController>(Lifetime.Scoped);

            builder.RegisterEntryPoint<AuthFlowManager>();

            builder.RegisterEntryPoint<UnifiedEntryPoint>();
        }
    }
}