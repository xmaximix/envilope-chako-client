using EnvilopeChako.Common;
using EnvilopeChako.Modules.Authentication.Presentation.ViewModels;
using R3;
using Stateless;
using VContainer.Unity;

namespace EnvilopeChako.Modules.Authentication.Flow
{
    public enum AuthState { Login, Register, Verify }
    public enum AuthTrigger { LoggedIn, Registered, Verified }

    public class AuthFlow : IInitializable, System.IDisposable
    {
        private readonly StateMachine<AuthState, AuthTrigger> _sm;
        public ReadOnlyReactiveProperty<AuthState> State { get; }
        private DisposableBag _bag;
        private readonly ILogger _log;

        public AuthFlow(LoginViewModel loginVm,
            RegisterViewModel registerVm,
            VerifyViewModel verifyVm,
            ILogger log)
        {
            _log = log;
            _sm = new StateMachine<AuthState, AuthTrigger>(AuthState.Login);
            _sm.Configure(AuthState.Login)
                .Permit(AuthTrigger.LoggedIn, AuthState.Register);
            _sm.Configure(AuthState.Register)
                .Permit(AuthTrigger.Registered, AuthState.Verify);
            _sm.Configure(AuthState.Verify)
                .OnEntry(() => _log.Info("Authentication complete"));
            
            var rp = new ReactiveProperty<AuthState>(_sm.State);
            _sm.OnTransitioned(transition =>
            {
                rp.Value = transition.Destination;
            });

            State = rp
                .ToReadOnlyReactiveProperty()
                .AddTo(ref _bag);

            loginVm.OnSuccess.Subscribe(_ => _sm.Fire(AuthTrigger.LoggedIn)).AddTo(ref _bag);
            registerVm.OnInitiated.Subscribe(_ => _sm.Fire(AuthTrigger.Registered)).AddTo(ref _bag);
            verifyVm.OnSuccess.Subscribe(_ => _sm.Fire(AuthTrigger.Verified)).AddTo(ref _bag);
        }

        public void Initialize()
        {
            _log.Info($"AuthFlow starting at state {_sm.State}");
        }

        public void Dispose() => _bag.Dispose();
    }
}