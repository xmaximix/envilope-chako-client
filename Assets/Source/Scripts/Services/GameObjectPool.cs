using System.Collections.Generic;
using UnityEngine;

namespace EnvilopeChako.Services
{
    public interface IPool<T> where T : Component
    {
        T Get();
        void Release(T item);
    }

    public class GameObjectPool<T> : IPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Stack<T> _stack = new();
        private readonly Transform _parent;

        public GameObjectPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Get()
        {
            if (_stack.Count > 0)
            {
                var item = _stack.Pop();
                item.gameObject.SetActive(true);
                return item;
            }
            return Object.Instantiate(_prefab, _parent);
        }

        public void Release(T item)
        {
            item.gameObject.SetActive(false);
            _stack.Push(item);
        }
    }
}