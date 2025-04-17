using System.Threading;
using Cysharp.Threading.Tasks;
using EnvilopeChako.Common;
using EnvilopeChako.Modules.Authentication.Application;
using R3;

namespace EnvilopeChako.Modules.Authentication.Presentation.ViewModels
{
    public class VerifyViewModel : System.IDisposable
    {
        public ReactiveProperty<string> Code { get; }
        public ReadOnlyReactiveProperty<bool> CanSubmit { get; }

        private readonly Subject<Unit> _success = new Subject<Unit>();
        public Observable<Unit> OnSuccess => _success;

        private readonly IVerifyUseCase _useCase;
        private readonly ILogger         _log;
        private DisposableBag            _bag;

        public VerifyViewModel(IVerifyUseCase useCase, ILogger log)
        {
            _useCase = useCase;
            _log     = log;

            Code = new ReactiveProperty<string>(string.Empty);
            _bag = new DisposableBag();

            CanSubmit = Code
                .Select(c => !string.IsNullOrWhiteSpace(c))
                .ToReadOnlyReactiveProperty()
                .AddTo(ref _bag);
        }

        public async UniTask SubmitAsync(CancellationToken ct)
        {
            _log.Info("Verification attempt");
            var res = await _useCase
                .Execute(Code.Value)
                .AttachExternalCancellation(ct);

            if (res.IsSuccess)
            {
                _log.Info("Verification succeeded");
                _success.OnNext(Unit.Default);
            }
            else
            {
                ErrorBus.Push(res.Error);
                _log.Error("Verification failed", new System.Exception(res.Error.Message));
            }
        }

        public void Dispose()
        {
            _bag.Dispose();
        }
    }
}