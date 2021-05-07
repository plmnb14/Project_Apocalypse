using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataStruct : MonoBehaviour
{
    static public void SaveData<T>(T singleData, string jsonDataName)
    {
        string jsonData = JsonUtility.ToJson(singleData, true);
        string path = Application.dataPath + "/Data/Json/" + jsonDataName + ".json";
        File.WriteAllText(path, jsonData);
    }

    static public void SaveData<T>(List<T> list, string jsonDataName)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<T>(list), true);
        string path = Application.dataPath + "/Data/Json/" + jsonDataName + ".json";
        File.WriteAllText(path, jsonData);
    }

    static public void SaveData<TKey, TValue>(Dictionary<TKey, TValue> dic, string jsonDataName)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<TKey, TValue>(dic), true);
        string path = Application.dataPath + "/Data/Json/" + jsonDataName + ".json";
        File.WriteAllText(path, jsonData);
    }

    static public void LoadData<T>(out T data, string dataName)
    {
        string path = Application.dataPath + "/Data/Json/" + dataName + ".json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<T>(jsonData);
    }

    static public void LoadData<T>(out List<T> list, string dataName)
    {
        string path = Application.dataPath + "/Data/Json/" + dataName + ".json";
        string jsonData = File.ReadAllText(path);
        list = JsonUtility.FromJson<Serialization<T>>(jsonData).ToList();
    }

    static public void LoadData<TKey, TValue>(out Dictionary<TKey, TValue> dic, string dataName)
    {
        string path = Application.dataPath + "/Data/Json/" + dataName + ".json";
        string jsonData = File.ReadAllText(path);
        dic = JsonUtility.FromJson<Serialization<TKey, TValue>>(jsonData).ToDictionary();
    }
}

[System.Serializable]
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

[System.Serializable]
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
        for(var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}

[System.Serializable]
public class BaseData
{
    public string objectName;

    public virtual BaseData DeepCopy(BaseData data = null)
    {
        BaseData copy = data == null ? new BaseData() : data;
        copy.objectName = this.objectName;

        return copy;
    }
}

[System.Serializable]
public class LivingData : BaseData
{
    public int level;
    public int EXP;
    public int damage;
    public int hitPoint;
    public int armor;
    public float attackSpeed;
    public float hitPointRegen;

    public override BaseData DeepCopy(BaseData data = null)
    {
        LivingData copy = data == null ? new LivingData() : (LivingData)data;
        base.DeepCopy(copy);
        copy.damage = this.damage;
        copy.hitPoint = this.hitPoint;
        copy.armor = this.armor;
        copy.attackSpeed = this.attackSpeed;
        copy.hitPointRegen = this.hitPointRegen;
        copy.level = this.level;
        copy.EXP = this.EXP;

        return copy;
    }
}

[System.Serializable]
public class HeroData : LivingData
{
    public float dodge;
    public float criticalChance;
    public float armorPierce;
    public float buffDuration;
    public float debuffResist;
    public float criticalDamage;
    public float minDamange;
    public int moreDamage;
    public int gainItem;
    public int gainEXP;
    public int gainGold;

    public override BaseData DeepCopy(BaseData data = null)
    {
        HeroData copy = data == null ? new HeroData() : (HeroData)data;
        base.DeepCopy(copy);
        copy.gainItem = this.gainItem;
        copy.gainEXP = this.gainEXP;
        copy.gainGold = this.gainGold;
        copy.criticalChance = this.criticalChance;
        copy.criticalDamage = this.criticalDamage;
        copy.moreDamage = this.moreDamage;
        copy.minDamange = this.minDamange;
        copy.buffDuration = this.buffDuration;
        copy.debuffResist = this.debuffResist;
        copy.armorPierce = this.armorPierce;

        return copy;
    }

}