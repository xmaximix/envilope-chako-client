using System;
using EnvilopeChako.Authentication;
using R3;
using VContainer.Unity;

namespace EnvilopeChako.Common
{
    public class MenuEntryPoint : IStartable, IDisposable
    {
        private readonly AuthFlowManager authFlowManager;
        private readonly CompositeDisposable disposables = new();

        public MenuEntryPoint(AuthFlowManager authFlowManager)
        {
            this.authFlowManager = authFlowManager;
        }

        public void Start()
        {
            authFlowManager.Initialize();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}