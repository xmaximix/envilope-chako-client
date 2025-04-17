using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using EnvilopeChako.Modules.Authentication.Application;
using R3;

namespace EnvilopeChako.Modules.Authentication.Presentation.ViewModels
{
    public class LoginViewModel : IDisposable
    {
        public ReactiveProperty<string> Email { get; } = new("...");
        public ReactiveProperty<string> Password { get; } = new("...");
        public ReadOnlyReactiveProperty<bool> CanSubmit { get; }
        private readonly Subject<Unit> _success = new();
        public Observable<Unit> OnSuccess => _success;

        private readonly ILoginUseCase _useCase;
        private readonly IFeatureToggleService _flags;
        private readonly ILogger _log;
        private readonly DisposableBag _bag;

        public LoginViewModel(ILoginUseCase useCase, IFeatureToggleService flags, ILogger log)
        {
            _useCase = useCase;
            _flags = flags;
            _log = log;

            CanSubmit = Disposable.AddTo(Observable
                    .CombineLatest(Email, Password, (e, p) => !string.IsNullOrWhiteSpace(e) && p.Length >= 6)
                    .ToReadOnlyReactiveProperty(), ref _bag);
        }

        public async UniTask SubmitAsync(CancellationToken ct)
        {
            if (!_flags.IsEnabled("NewLoginFlow"))
                _log.Warn("Using legacy login flow");

            _log.Info("Login attempt");
            var res = await _useCase.Execute(Email.Value, Password.Value)
                .AttachExternalCancellation(ct);

            if (res.IsSuccess)
            {
                _log.Info("Login succeeded");
                _success.OnNext(Unit.Default);
            }
            else
            {
                ErrorBus.Push(res.Error);
                _log.Error("Login failed", new System.Exception(res.Error.Message));
            }
        }

        public void Dispose() => _bag.Dispose();
    }
}