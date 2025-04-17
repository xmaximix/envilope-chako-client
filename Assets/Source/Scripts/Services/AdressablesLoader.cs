using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace EnvilopeChako.Services
{
    public readonly struct AddressableHandle<T> where T : Component
    {
        public readonly AsyncOperationHandle<GameObject> Handle;
        public readonly T Instance;

        public AddressableHandle(AsyncOperationHandle<GameObject> handle, T instance)
        {
            Handle = handle;
            Instance = instance;
        }

        public void Release()
        {
            if (Handle.IsValid() && Instance != null)
            {
                try
                {
                    Addressables.ReleaseInstance(Instance.gameObject);
                }
                catch (MissingReferenceException)
                {
                }
            }
        }
    }

    public class AddressablesLoader
    {
        public static async UniTask<AddressableHandle<T>> LoadAsync<T>(
            string key,
            Transform parent = null
        ) where T : Component
        {
            var handle = Addressables.InstantiateAsync(key, parent);
            await handle.Task;
            var comp = handle.Result.GetComponent<T>();
            return new AddressableHandle<T>(handle, comp);
        }

        public static async UniTask<SceneInstance> LoadSceneAsync(string key)
        {
            var handle = Addressables.LoadSceneAsync(key);
            await handle.Task;
            return handle.Result;
        }
    }
}