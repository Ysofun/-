#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using FrameworkDesign;

namespace CounterApp
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);

        int LoadInt(string key, int defalutValue = 0);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public int LoadInt(string key, int defalutValue = 0)
        {
            return PlayerPrefs.GetInt(key);
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }

    public class EditorPrefsStorage : IStorage
    {
        public int LoadInt(string key, int defalutValue = 0)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt(key);
#else
            return 0
#endif
        }

        public void SaveInt(string key, int value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt(key, value);
#endif
        }
    }
}