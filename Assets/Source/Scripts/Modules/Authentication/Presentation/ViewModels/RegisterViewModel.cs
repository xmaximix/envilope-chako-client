using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using EnvilopeChako.Modules.Authentication.Application;
using R3;

namespace EnvilopeChako.Modules.Authentication.Presentation.ViewModels
{
    public class RegisterViewModel : System.IDisposable
    {
        public ReactiveProperty<string> Nickname  { get; }
        public ReactiveProperty<string> Email     { get; }
        public ReactiveProperty<string> Password  { get; }
        public ReadOnlyReactiveProperty<bool> CanSubmit { get; }

        private readonly Subject<Unit> _initiated = new Subject<Unit>();
        public Observable<Unit> OnInitiated => _initiated;

        private readonly IRegisterUseCase _useCase;
        private readonly ILogger           _log;
        private DisposableBag              _bag;

        public RegisterViewModel(IRegisterUseCase useCase, ILogger log)
        {
            _useCase = useCase;
            _log     = log;

            Nickname = new ReactiveProperty<string>(string.Empty);
            Email    = new ReactiveProperty<string>(string.Empty);
            Password = new ReactiveProperty<string>(string.Empty);

            _bag = new DisposableBag();

            CanSubmit = Observable
                .CombineLatest(Nickname, Email, Password, 
                    (n, e, p) => 
                        !string.IsNullOrWhiteSpace(n) && 
                        !string.IsNullOrWhiteSpace(e) && 
                        p.Length >= 6)
                .ToReadOnlyReactiveProperty()
                .AddTo(ref _bag);
        }

        public async UniTask SubmitAsync(CancellationToken ct)
        {
            _log.Info("Registration attempt");
            var res = await _useCase
                .Execute(Nickname.Value, Email.Value, Password.Value)
                .AttachExternalCancellation(ct);

            if (res.IsSuccess)
            {
                _log.Info("Registration initiated");
                _initiated.OnNext(Unit.Default);
            }
            else
            {
                ErrorBus.Push(res.Error);
                _log.Error("Registration failed", new System.Exception(res.Error.Message));
            }
        }

        public void Dispose()
        {
            _bag.Dispose();
        }
    }
}