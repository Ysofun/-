using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IStorage : IUtility
    {
        void SaveInt(string name, int value);
        int LoadInt(string name, int defaultValue = 0);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public int LoadInt(string name, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(name, defaultValue);
        }

        public void SaveInt(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
        }
    }
}