using EnvilopeChako.Modules.Authentication.Domain;
using R3;

namespace EnvilopeChako.Common
{
    public static class ErrorBus
    {
        private static readonly Subject<Error> _bus = new();
        public static Observable<Error> Errors => _bus;
        public static void Push(Error err)
        {
            _bus.OnNext(err);
        }
    }
}