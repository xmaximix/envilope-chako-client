using UnityEngine;

namespace EnvilopeChako.Common
{
    public interface ILogger
    {
        void Info(string msg);
        void Warn(string msg);
        void Error(string msg, System.Exception ex = null);
    }

    public class UnityLogger : ILogger
    {
        public void Info(string msg)
        {
            Debug.Log(msg);
        }
        public void Warn(string msg)
        {
            Debug.LogWarning(msg);
        }
        public void Error(string msg, System.Exception ex = null)
        {
            Debug.LogError(msg + (ex != null ? "\n" + ex : string.Empty));
        }
    }
}