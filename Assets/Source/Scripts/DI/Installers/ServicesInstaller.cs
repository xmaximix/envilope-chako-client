using EnvilopeChako.Services;
using UnityEngine;
using VContainer;

namespace EnvilopeChako.DI.Installers
{
    public class ServicesInstaller : BaseInstaller
    {
        [Header("Backend Config URL")]
        [SerializeField] private string configUrl = "https://api.example.com/client-config";

        public override void Install(IContainerBuilder builder)
        {
            builder.Register<IConfigService, HttpConfigService>(Lifetime.Singleton)
                   .WithParameter("url", configUrl);

            builder.Register<AddressablesLoader>(Lifetime.Singleton);
        }
    }
}