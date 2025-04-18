using System.Collections.Generic;

namespace EnvilopeChako.Common
{
    public static class ConfigDTOExtensions
    {
        public static Dictionary<string, string> ToDict(this ConfigDTO dto)
        {
            var dict = new Dictionary<string, string>();
            if (dto.entries == null) return dict;
            foreach (var e in dto.entries)
                dict[e.key] = e.value;
            return dict;
        }
    }
}