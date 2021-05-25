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
    public List<GachaChanceData> weaponGachaChanceList { get; set; }
    public List<WeaponStatsForDatabase> weaponStatForDataList { get; set; }
    public Dictionary<int, WeaponStatsForDatabase> weaponStatForDataDictionary { get; set; }
    #endregion

    private void LoadCSVData()
    {
        CSVReader.GetGachaChanceDataListOnCSVFile(out List<GachaChanceData> gachaList, "WeaponGachaChanceData.csv");
        weaponGachaChanceList = gachaList;

        CSVReader.GetWeaponStatsForDataBaseListOnCSVFile(out List<WeaponStatsForDatabase> statusList, "WeaponStatusBaseData.csv");
        weaponStatForDataList = statusList;

        CSVReader.GetWeaponStatsForDataBaseDictionaryOnCSVFile(out Dictionary<int, WeaponStatsForDatabase> statusDictionary, "weaponStatusBaseData.csv");
        weaponStatForDataDictionary = statusDictionary;
    }

    private void SetUpField()
    {
        weaponGachaChanceList = new List<GachaChanceData>();
        weaponStatForDataList = new List<WeaponStatsForDatabase>();
        weaponStatForDataDictionary = new Dictionary<int, WeaponStatsForDatabase>();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);

        SetUpField();
        LoadCSVData();
    }
}
