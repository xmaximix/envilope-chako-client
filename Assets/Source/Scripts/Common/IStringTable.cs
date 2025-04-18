namespace EnvilopeChako.Common
{
    public interface IStringTable
    {
        string Get(string key);
    }

    [System.Serializable]
    public struct LocalizationEntry
    {
        public string Key;
        public string Value;
    }
}