using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DBType { Weapon, Drone, Arms, Scripts, Skill , End }

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
    public List<WeaponEnchantTable> weaponEnchantTableList { get; set; }
    public List<DroneStatusForDB> droneStatusDBList { get; set; }
    public Dictionary<int, DroneStatusForDB> droneStatusDictionary { get; set; }
    public Dictionary<string, string> scriptsDictionary { get; set; }
    public Dictionary<int, ArmsStatusForDB> armsDBDic { get; set; }
    public Dictionary<int, SkillStatusDB> skillDBDic { get; set; }
    #endregion

    private void LoadCSVData()
    {
        CSVReader.GetGachaChanceDataListOnCSVFile(out List<GachaChanceData> gachaList, "WeaponGachaChanceData.csv");
        weaponGachaChanceList = gachaList;

        CSVReader.GetWeaponStatsForDataBaseListOnCSVFile(out List<WeaponStatsForDatabase> statusList, "WeaponStatusBaseData.csv");
        weaponStatForDataList = statusList;

        CSVReader.GetWeaponStatsForDataBaseDictionaryOnCSVFile(out Dictionary<int, WeaponStatsForDatabase> statusDictionary, "weaponStatusBaseData.csv");
        weaponStatForDataDictionary = statusDictionary;

        CSVReader.GetWeaponEnchantTableListOnCSVFile(out List<WeaponEnchantTable> enchantList, "WeaponEnchantData.csv");
        weaponEnchantTableList = enchantList;

        CSVReader.GetDroneStatusListOnCSV(out List<DroneStatusForDB> droneStatusList, "DroneStatusData.csv");
        droneStatusDBList = droneStatusList;

        CSVReader.GetScriptsDictionaryOnCSV(out Dictionary<string, string> dataDictionary, "NotificationScriptsData.csv");
        scriptsDictionary = dataDictionary;

        CSVReader.GetArmsDBOnCSV(out Dictionary<int, ArmsStatusForDB> armsDic, "ArmsData.csv");
        armsDBDic = armsDic;

        CSVReader.GetSkillDBOnCSV(out Dictionary<int, SkillStatusDB> skillDic, "SkillData.csv");
        skillDBDic = skillDic;
    }

    private void SetUpField()
    {
        weaponGachaChanceList = new List<GachaChanceData>();
        weaponStatForDataList = new List<WeaponStatsForDatabase>();
        weaponStatForDataDictionary = new Dictionary<int, WeaponStatsForDatabase>();
        weaponEnchantTableList = new List<WeaponEnchantTable>();
        droneStatusDBList = new List<DroneStatusForDB>();
        scriptsDictionary = new Dictionary<string, string>();
        armsDBDic = new Dictionary<int, ArmsStatusForDB>();
        skillDBDic = new Dictionary<int, SkillStatusDB>();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);

        SetUpField();
        LoadCSVData();
    }
}
