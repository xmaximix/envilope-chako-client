using EnvilopeChako.Common;
using EnvilopeChako.DI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.Authentication
{
    public class AuthInstaller : BaseInstaller
    {
        [Header("UI References")]
        [SerializeField] private LoginView loginView;       
        [SerializeField] private RegisterView registerView; 
        [SerializeField] private VerificationView verificationView;

        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance<ILoginView>(loginView);
            builder.RegisterInstance<IRegisterView>(registerView);
            builder.RegisterInstance<IVerificationView>(verificationView);

            builder.Register<IAuthService, DummyAuthService>(Lifetime.Singleton);

            builder.Register<LoginController>(Lifetime.Scoped);
            builder.Register<RegisterController>(Lifetime.Scoped);
            builder.Register<VerificationController>(Lifetime.Scoped);
            
            builder.Register<AuthFlowManager>(Lifetime.Singleton);
        }
    }
}