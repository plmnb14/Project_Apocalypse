using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManger : MonoBehaviour
{
    #region 인스턴스
    static public DataManger instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<DataManger>();

            return m_instance;
        }
    }
    private static DataManger m_instance;
    #endregion

    #region 필드
    public List<EquipmentGachaData> equipmentGachaDataList { get; set; }
    public List<PlayerEquipmentItemData> playerEquipmentItemDataList { get; set; }
    #endregion

    private void LoadCSVData()
    {
        CSVReader.GetEquipmentGachaListOnCSVFile(out List<EquipmentGachaData> gachaList, "EquipmentGachaChanceData.csv");
        equipmentGachaDataList = gachaList;
        CSVReader.GetEquipmentStatusListOnCSVFile(out List<PlayerEquipmentItemData> statusList, "EquipmentStatusData.csv");
        playerEquipmentItemDataList = statusList;
    }

    private void SetUpField()
    {
        equipmentGachaDataList = new List<EquipmentGachaData>();
        playerEquipmentItemDataList = new List<PlayerEquipmentItemData>();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);

        SetUpField();
        LoadCSVData();
    }
}
