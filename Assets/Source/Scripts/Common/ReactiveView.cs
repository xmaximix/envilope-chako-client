using R3;
using UnityEngine;

namespace EnvilopeChako.Common
{
    public abstract class ReactiveView<T> : MonoBehaviour where T : System.IDisposable
    {
        protected T ViewModel;
        private DisposableBag _bag;

        public void Init(T vm)
        {
            ViewModel = vm;
            Bind();
        }

        protected abstract void Bind();

        protected void AddSubscription(System.IDisposable d)
        {
            d.AddTo(ref _bag);
        }

        protected virtual void OnDestroy()
        {
            _bag.Dispose();
            ViewModel?.Dispose();
        }
    }
}