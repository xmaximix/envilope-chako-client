using System;

namespace EnvilopeChako.Common
{
    [Serializable]
    public class ConfigEntry
    {
        public string key;
        public string value;
    }

    [Serializable]
    public class ConfigDTO
    {
        public ConfigEntry[] entries;
    }
}