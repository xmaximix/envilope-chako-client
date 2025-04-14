using R3;
using UnityEngine.UI;

namespace EnvilopeChako.Extensions
{
    public static class ButtonExtensions
    {
        public static Observable<Unit> OnClickAsObservable(this Button button)
        {
            return Observable.FromEvent<UnityEngine.Events.UnityAction, Unit>(
                conversion: handler => () => handler(Unit.Default),
                addHandler: h => button.onClick.AddListener(h),
                removeHandler: h => button.onClick.RemoveListener(h)
            );
        }
    }

}