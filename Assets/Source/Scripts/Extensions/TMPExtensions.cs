using System.Threading;
using R3;
using TMPro;
using UnityEngine.Events;

namespace EnvilopeChako.Extensions
{
    public static class TMPObservableExtensions
    {
        public static Observable<string> OnValueChangedAsObservable(
            this TMP_InputField inputField,
            CancellationToken cancellationToken = default)
        {
            var obs = Observable.FromEvent<UnityAction<string>, string>(
                handler => new UnityAction<string>(handler),
                h => inputField.onValueChanged.AddListener(h),
                h => inputField.onValueChanged.RemoveListener(h)
            );

            return cancellationToken == default
                ? obs
                : obs.TakeUntil(cancellationToken);
        }
    }
}