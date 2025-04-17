using System;
using R3;

namespace EnvilopeChako.Common
{
    public class AnalyticsErrorReporter : IDisposable
    {
        private readonly IDisposable _sub;
        private readonly ILogger _log;

        public AnalyticsErrorReporter(ILogger log)
        {
            _log = log;
            _sub = ErrorBus.Errors.Subscribe(err => _log.Info($"[Analytics] {err.Message}"));
        }

        public void Dispose()
        {
            _sub.Dispose();
        }
    }
}