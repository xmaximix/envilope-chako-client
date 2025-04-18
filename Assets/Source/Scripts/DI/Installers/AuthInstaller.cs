using EnvilopeChako.Modules.Authentication.Adapters;
using EnvilopeChako.Modules.Authentication.Application;
using EnvilopeChako.Modules.Authentication.Domain;
using EnvilopeChako.Modules.Authentication.Flow;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.DI.Installers
{
    public class AuthInstaller : BaseInstaller
    {
        [SerializeField] private AuthCanvas authCanvas;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(authCanvas);
            
            builder.Register<IAuthRepository, DummyAuthRepository>(Lifetime.Singleton);

            builder.Register<ILoginUseCase, LoginUseCase>(Lifetime.Singleton);
            builder.Register<IRegisterUseCase, RegisterUseCase>(Lifetime.Singleton);
            builder.Register<IVerifyUseCase, VerifyUseCase>(Lifetime.Singleton);

            builder.Register<LoginViewModel>(Lifetime.Scoped);
            builder.Register<RegisterViewModel>(Lifetime.Scoped);
            builder.Register<VerifyViewModel>(Lifetime.Scoped);
            
            builder.Register<AuthFlow>(Lifetime.Singleton).As<IInitializable>();
        }
    }
}