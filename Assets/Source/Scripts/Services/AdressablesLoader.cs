using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using AsyncOperation = System.ComponentModel.AsyncOperation;

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
            Addressables.ReleaseInstance(Instance.gameObject);
        }
    }

    public static class AddressablesLoader
    {
        public static async UniTask<AddressableHandle<T>> LoadAsync<T>(string key) where T : Component
        {
            var h = Addressables.LoadAssetAsync<GameObject>(key);
            await h.Task;
            var go = Object.Instantiate(h.Result);
            var comp = go.GetComponent<T>();
            return new AddressableHandle<T>(h, comp);
        }

        public static async UniTask<SceneInstance> LoadSceneAsync(string key)
        {
            var handle = Addressables.LoadSceneAsync(key);
            await handle.Task;
            return handle.Result;
        }
    }
}