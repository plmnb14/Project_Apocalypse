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

    #region 바이너리 CSV
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
