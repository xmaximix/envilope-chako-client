using UnityEngine;

namespace EnvilopeChako.Common
{
    [CreateAssetMenu(menuName = "Config/LocalizationTable")]
    public class LocalizationTable : ScriptableObject, IStringTable
    {
        public LocalizationEntry[] entries;

        public string Get(string key)
        {
            foreach (var e in entries)
                if (e.Key == key)
                    return e.Value;
            return key;
        }
    }
}