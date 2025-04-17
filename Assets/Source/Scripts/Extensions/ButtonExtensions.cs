using R3;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Threading;

namespace EnvilopeChako.Extensions
{
    public static class ButtonExtensions
    {
        public static Observable<Unit> OnClickAsObservable(
            this Button button,
            CancellationToken cancellationToken = default)
        {
            var obs = Observable.FromEvent<UnityAction, Unit>(
                handler => () => handler(Unit.Default),
                h => button.onClick.AddListener(h),
                h => button.onClick.RemoveListener(h)
            );

            return cancellationToken == default
                ? obs
                : obs.TakeUntil(cancellationToken);
        }
    }
}