using UnityEngine;

namespace GameName.LoggingSystem
{
    public static class CLogger
    {
        public static void Log(object msg)
        {
#if UNITY_EDITOR
            Debug.Log(msg);
#endif
        }
        
        public static void LogWarning(object msg)
        {
#if UNITY_EDITOR
            Debug.LogWarning(msg);
#endif
        }

    }
}