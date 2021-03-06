using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    public static void GetGachaChanceDataListOnCSVFile(out List<GachaChanceData> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        List<GachaChanceData> gachaList = new List<GachaChanceData>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for(int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            GachaChanceData gachaData = new GachaChanceData
            {
                independenceChance = float.Parse(parts[0]),
                accumulateChance = int.Parse(parts[1]),
                itemCode = int.Parse(parts[2])
            };
            gachaList.Add(gachaData);
        }

        dataList = gachaList;
    }

    public static void GetWeaponStatsForDataBaseDictionaryOnCSVFile(out Dictionary<int, WeaponStatsForDatabase> dataDictionary, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        Dictionary<int, WeaponStatsForDatabase> statusDictionary = new Dictionary<int, WeaponStatsForDatabase>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            WeaponStatsForDatabase statusData = new WeaponStatsForDatabase
            {
                itemCode = int.Parse(parts[0]),
                weaponName = parts[1],
                grade = int.Parse(parts[2]),
                tier = int.Parse(parts[3]),
                reinforceCostOrigin = int.Parse(parts[4]),
                damagePercentOrigin = float.Parse(parts[5]),
                damageFixedOrigin = int.Parse(parts[6]),
                criticalChanceOrigin = float.Parse(parts[7]),
                criticalDamageOrigin = int.Parse(parts[8]),
                heldStatusType_0 = (HeldStatType)int.Parse(parts[9]),
                heldStatusType_1 = (HeldStatType)int.Parse(parts[10]),
                heldStatusType_2 = (HeldStatType)int.Parse(parts[11]),
                heldStatusValueOrigin_0 = float.Parse(parts[12]),
                heldStatusValueOrigin_1 = float.Parse(parts[13]),
                heldStatusValueOrigin_2 = float.Parse(parts[14])
            };
            
            statusDictionary.Add(int.Parse(parts[0]), statusData);
        }

        dataDictionary = statusDictionary;
    }

    public static void GetWeaponStatsForDataBaseListOnCSVFile(out List<WeaponStatsForDatabase> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        List<WeaponStatsForDatabase> statusList = new List<WeaponStatsForDatabase>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            WeaponStatsForDatabase statusData = new WeaponStatsForDatabase
            {
                itemCode = int.Parse(parts[0]),
                weaponName = parts[1],
                grade = int.Parse(parts[2]),
                tier = int.Parse(parts[3]),
                reinforceCostOrigin = int.Parse(parts[4]),
                damagePercentOrigin = float.Parse(parts[5]),
                damageFixedOrigin = int.Parse(parts[6]),
                criticalChanceOrigin = float.Parse(parts[7]),
                criticalDamageOrigin = int.Parse(parts[8]),
                heldStatusType_0 = (HeldStatType)int.Parse(parts[9]),
                heldStatusType_1 = (HeldStatType)int.Parse(parts[10]),
                heldStatusType_2 = (HeldStatType)int.Parse(parts[11]),
                heldStatusValueOrigin_0 = float.Parse(parts[12]),
                heldStatusValueOrigin_1 = float.Parse(parts[13]),
                heldStatusValueOrigin_2 = float.Parse(parts[14])
            };

            statusList.Add(statusData);
        }

        dataList = statusList;
    }

    public static void GetWeaponEnchantTableListOnCSVFile(out List<WeaponEnchantTable> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;
        List<WeaponEnchantTable> statsList = new List<WeaponEnchantTable>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            WeaponEnchantTable statusData = new WeaponEnchantTable
            {
                grade = int.Parse(parts[0]),
                chance = float.Parse(parts[1]),
                accumulateChance = int.Parse(parts[2]),
                damageFixed = int.Parse(parts[3]),
                damagePercent = float.Parse(parts[4]),
                criticalChance = float.Parse(parts[5]),
                criticalDamage = float.Parse(parts[6]),
                hitPoint = float.Parse(parts[7]),
                armor = float.Parse(parts[8])
            };

            statsList.Add(statusData);
        }

        dataList = statsList;
    }

    public static void GetDroneStatusListOnCSV(out List<DroneStatusForDB> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;
        List<DroneStatusForDB> statusList = new List<DroneStatusForDB>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            DroneStatusForDB statusData = new DroneStatusForDB();
            statusData.uniqueNumber = int.Parse(parts[0]);
            statusData.myName = parts[1];
            statusData.damagePercent = float.Parse(parts[2]);
            statusData.growthDamagePercent = float.Parse(parts[3]);
            statusData.attackSpeed = float.Parse(parts[4]);
            statusData.growthAttackSpeed = float.Parse(parts[5]);

            statusList.Add(statusData);
        }

        dataList = statusList;
    }

    public static void GetScriptsDictionaryOnCSV(out Dictionary<string, string> dataDictionary, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            dictionary.Add(parts[0], parts[1]);
        }

        dataDictionary = dictionary;
    }

    public static void GetArmsDBOnCSV(out Dictionary<int, ArmsStatusForDB> dataDic, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        Dictionary<int, ArmsStatusForDB> statusDic = new Dictionary<int, ArmsStatusForDB>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            ArmsStatusForDB statusData = new ArmsStatusForDB()
            {
                itemCode = int.Parse(parts[0]),
                armsType = int.Parse(parts[1]),
                itemName = parts[2],
                baseValue = float.Parse(parts[3]),
                growthValue = float.Parse(parts[4]),
                duration = float.Parse(parts[5]),
                explain = parts[6]
            };

            statusDic.Add(int.Parse(parts[0]), statusData);
        }

        dataDic = statusDic;
    }

    public static void GetSkillDBOnCSV(out Dictionary<int, SkillStatusDB> dataDic, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        Dictionary<int, SkillStatusDB> statusDic = new Dictionary<int, SkillStatusDB>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            SkillStatusDB statusData = new SkillStatusDB()
            {
                skillID = int.Parse(parts[0]),
                skillName = parts[1],
                duration = float.Parse(parts[2]),
                growthDuration = float.Parse(parts[3]),
                activeCount = float.Parse(parts[4]),
                growthActiveCount = float.Parse(parts[5]),
                percent = float.Parse(parts[6]),
                growthPercent = float.Parse(parts[7]),
                coolTime = float.Parse(parts[8]),
                growthCoolTime = float.Parse(parts[9]),
                cost = float.Parse(parts[10]),
                growthCost = float.Parse(parts[11]),
                costType = (skillCostType)int.Parse(parts[12]),
                isActiveSkill = int.Parse(parts[13]) > 0 ? true : false,
                explain = parts[14]
            };

            statusDic.Add(statusData.skillID, statusData);
        }

        dataDic = statusDic;
    }

    public static void GetSkillDBOnCSV(out List<SkillDBLoad> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        dataList = new List<SkillDBLoad>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            SkillDBLoad skillDB = new SkillDBLoad
            {
                skillActType = (SkillActType)int.Parse(parts[0]),
                skillDBName = parts[1]
            };
            dataList.Add(skillDB);
        }
    }
        #region ???????? CSV
        //public static List<EquipmentGachaData> GetEquipmentGachaListOnCSVFile(string fileName)
        //{
        //    string filePath = "Assets/Resources/Database/" + fileName;

        //    List<EquipmentGachaData> gachaList = new List<EquipmentGachaData>();
        //    byte[] bytes = File.ReadAllBytes(filePath);
        //    int rowSize = sizeof(int) * 3 + sizeof(float) * 1;
        //    Debug.Log(bytes.Length);
        //    for(int offset = 0; offset < bytes.Length; offset += rowSize)
        //    {
        //        EquipmentGachaData gachaData = new EquipmentGachaData
        //        {
        //            gachaChance = BitConverter.ToSingle(bytes, offset + 0),
        //            inhenceGachaChance = BitConverter.ToInt32(bytes, offset + 4),
        //            accumulateGachaChance = BitConverter.ToInt32(bytes, offset + 8),
        //            itemCode = BitConverter.ToInt32(bytes, offset + 12)
        //        };
        //        gachaList.Add(gachaData);
        //    }

        //    return gachaList;
        //}
        #endregion
    }
