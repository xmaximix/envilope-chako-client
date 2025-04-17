using UnityEngine;

namespace EnvilopeChako.Common
{
    public interface IStringTable
    {
        string Get(string key);
    }

    [System.Serializable]
    public struct LocalizationEntry { public string Key; public string Value; }

    [CreateAssetMenu(menuName = "Config/LocalizationTable")]
    public class LocalizationTable : ScriptableObject, IStringTable
    {
        public LocalizationEntry[] entries;

        public string Get(string key)
        {
            foreach (var e in entries)
                if (e.Key == key) return e.Value;
            return key;
        }
    }
}