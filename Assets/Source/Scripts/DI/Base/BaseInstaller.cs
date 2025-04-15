using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EnvilopeChako.DI
{
    public abstract class BaseInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}