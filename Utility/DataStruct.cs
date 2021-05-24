using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public struct EquipmentGachaData
{
    public float gachaChance;
    public int inhenceGachaChance;
    public int accumulateGachaChance;
    public int itemCode;
}

public struct BaseEquipmentItemData
{
    public int itemCode;
    public string itemName;
    public byte grade;
    public byte tier;
}

public struct PlayerEquipmentItemData
{
    public int itemCode;
    public string itemName;
    public int grade;
    public int tier;
    public int equipDamagePercent;
    public int equipDamageAdd;
    public float equipCriticalChance;
    public int equipCriticalDamage;
    public int heldStatusType_0;
    public int heldStatusType_1;
    public int heldStatusType_2;
    public float heldStatusValue_0;
    public float heldStatusValue_1;
    public float heldStatusValue_2;
}

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
    public long damage;
    public int hitPoint;
    public int armor;
    public float attackSpeed;
    public float hitPointRegen;
    public float dodge;
    public float criticalDamage;
    public float criticalChance;
    public float debuffResist;

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
        copy.dodge = this.dodge;
        copy.criticalChance = this.criticalChance;
        copy.criticalDamage = this.criticalDamage;
        copy.debuffResist = this.debuffResist;

        return copy;
    }
}

[System.Serializable]
public class HeroData : LivingData
{
    public float armorPierce;
    public float buffDuration;
    public float minDamage;
    public int moreDamage;
    public int dropItem;
    public int gainExp;
    public int gainGold;

    public override BaseData DeepCopy(BaseData data = null)
    {
        HeroData copy = data == null ? new HeroData() : (HeroData)data;
        base.DeepCopy(copy);
        copy.dropItem = this.dropItem;
        copy.gainExp = this.gainExp;
        copy.gainGold = this.gainGold;
        copy.moreDamage = this.moreDamage;
        copy.minDamage = this.minDamage;
        copy.buffDuration = this.buffDuration;
        copy.armorPierce = this.armorPierce;

        return copy;
    }

    public void AddData(ref HeroData data)
    {
        this.level += data.level;
        this.EXP += data.EXP;
        this.damage += data.damage;
        this.hitPoint += data.hitPoint;
        this.armor += data.armor;
        this.attackSpeed += data.attackSpeed;
        this.hitPointRegen += data.hitPointRegen;

        this.dodge += data.dodge;
        this.dropItem += data.dropItem;
        this.gainExp += data.gainExp;
        this.gainGold += data.gainGold;
        this.criticalChance += data.criticalChance;
        this.criticalDamage += data.criticalDamage;
        this.armorPierce += data.armorPierce;
        this.buffDuration += data.buffDuration;
        this.moreDamage += data.moreDamage;
        this.minDamage += data.minDamage;
        this.debuffResist += data.debuffResist;
    }

    public void ReplaceData(ref HeroData data)
    {
        this.level = data.level;
        this.EXP = data.EXP;
        this.damage = data.damage;
        this.hitPoint = data.hitPoint;
        this.armor = data.armor;
        this.attackSpeed = data.attackSpeed;
        this.hitPointRegen = data.hitPointRegen;

        this.dodge = data.dodge;
        this.dropItem = data.dropItem;
        this.gainExp = data.gainExp;
        this.gainGold = data.gainGold;
        this.criticalChance = data.criticalChance;
        this.criticalDamage = data.criticalDamage;
        this.armorPierce = data.armorPierce;
        this.buffDuration = data.buffDuration;
        this.moreDamage = data.moreDamage;
        this.minDamage += data.minDamage;
        this.debuffResist = data.debuffResist;
    }
}