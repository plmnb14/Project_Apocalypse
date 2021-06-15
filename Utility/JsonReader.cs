using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    #region Private Fields
    static private readonly int maxStringSize = 100;
    #endregion

    #region Save Events
    static public void SaveData<T>(T singleData, string fileName)
    {
        string jsonData = JsonUtility.ToJson(singleData, true);
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        File.WriteAllText(stringBuilder.ToString(), jsonData);
    }

    static public void SaveData<T>(List<T> listData, string fileName)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<T>(listData), true);
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        File.WriteAllText(stringBuilder.ToString(), jsonData);
    }

    static public void SaveData<TKey, TValue>(Dictionary<TKey, TValue> dictData, string fileName)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<TKey, TValue>(dictData), true);
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        File.WriteAllText(stringBuilder.ToString(), jsonData);
    }
    #endregion

    #region Load Events
    static public void LoadData<T>(out T singleData, string fileName)
    {
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        string jsonData = File.ReadAllText(stringBuilder.ToString());
        singleData = JsonUtility.FromJson<T>(jsonData);
    }

    static public void LoadData<T>(out List<T> listData, string fileName)
    {
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        string jsonData = File.ReadAllText(stringBuilder.ToString());
        listData = JsonUtility.FromJson<Serialization<T>>(jsonData).ToList();
    }

    static public void LoadData<TKey, TValue>(out Dictionary<TKey, TValue> dictData, string fileName)
    {
        var stringBuilder = new StringBuilder(maxStringSize, maxStringSize);
        stringBuilder.Append(Application.dataPath);
        stringBuilder.Append("/Data/Json/");
        stringBuilder.Append(fileName);
        stringBuilder.Append(".json");
        string jsonData = File.ReadAllText(stringBuilder.ToString());
        dictData = JsonUtility.FromJson<Serialization<TKey, TValue>>(jsonData).ToDictionary();
    }
    #endregion

    #region Serilize Events
    [Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;

        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    [Serializable]
    public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        List<TKey> keys;
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;
        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            var count = Mathf.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }
    }
    #endregion
}
