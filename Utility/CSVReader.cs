using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    public static void GetEquipmentGachaListOnCSVFile(out List<EquipmentGachaData> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        List<EquipmentGachaData> gachaList = new List<EquipmentGachaData>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for(int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            EquipmentGachaData gachaData = new EquipmentGachaData
            {
                gachaChance = float.Parse(parts[0]),
                inhenceGachaChance = int.Parse(parts[1]),
                accumulateGachaChance = int.Parse(parts[2]),
                itemCode = int.Parse(parts[3])
            };
            gachaList.Add(gachaData);
        }

        dataList = gachaList;
    }

    public static void GetEquipmentStatusListOnCSVFile(out List<PlayerEquipmentItemData> dataList, string fileName)
    {
        string filePath = "Assets/Resources/Database/" + fileName;

        List<PlayerEquipmentItemData> statusList = new List<PlayerEquipmentItemData>();
        var datas = File.ReadAllLines(filePath);
        int dataLength = datas.Length;
        for (int i = 1; i < dataLength; i++)
        {
            var parts = datas[i].Split(',');
            PlayerEquipmentItemData statusData = new PlayerEquipmentItemData
            {
                itemCode = int.Parse(parts[0]),
                itemName = parts[1],
                grade = int.Parse(parts[2]),
                tier = int.Parse(parts[3]),
                equipDamagePercent = int.Parse(parts[4]),
                equipDamageAdd = int.Parse(parts[5]),
                equipCriticalChance = float.Parse(parts[6]),
                equipCriticalDamage = int.Parse(parts[7]),
                heldStatusType_0 = int.Parse(parts[8]),
                heldStatusValue_0 = float.Parse(parts[9]),
                heldStatusType_1 = int.Parse(parts[10]),
                heldStatusValue_1 = float.Parse(parts[11]),
                heldStatusType_2 = int.Parse(parts[12]),
                heldStatusValue_2 = float.Parse(parts[13])
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
