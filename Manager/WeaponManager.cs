using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region 인스턴스
    static public WeaponManager instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = FindObjectOfType<WeaponManager>();
            }

            return _instance;
        }
    }
    static private WeaponManager _instance;
    #endregion

    #region 장비
    private int weaponChildCount;
    private Weapon[] arrayEquipment;
    #endregion

    public WeaponStatsForLocal allWeaponsStats;
    public WeaponDetailInfo weaponDetail { get; set; }
    private PopUpUI weaponDetailPopUp;

    public int mountedIndex { get; set; }

    public void UpdateWeaponStatsToPlayer(WeaponStatType statType)
    {
        HeroStatsEnum heroStat = HeroStatsEnum.DamageFixed;
        double updateValue = 0;
        switch (statType)
        {
            case WeaponStatType.DamageFixed:
                {
                    heroStat = HeroStatsEnum.DamageFixed;
                    updateValue = (double)allWeaponsStats.damageFixed;
                    break;
                }
            case WeaponStatType.DamagePercent:
                {
                    heroStat = HeroStatsEnum.DamagePercent;
                    updateValue = (double)allWeaponsStats.damagePercent;
                    break;
                }
            case WeaponStatType.CriticalChance:
                {
                    heroStat = HeroStatsEnum.Critical;
                    updateValue = (double)allWeaponsStats.criticalChance;
                    break;
                }
            case WeaponStatType.CriticalDamage:
                {
                    heroStat = HeroStatsEnum.CriticalDamage;
                    updateValue = (double)allWeaponsStats.criticalDamage;
                    break;
                }
        }

        PlayerStatusManager.instance.UpdateValue(heroStat, updateValue);
    }

    public void UpdateCount(int itemCode)
    {
        int equipmentIndex = FindEquipmentIndex(itemCode);
        if (!arrayEquipment[equipmentIndex].CheckUnlocked())
        {
            arrayEquipment[equipmentIndex].UnLockUpdate(true, true);
            arrayEquipment[equipmentIndex].SetHeldCount(0);
        }

        else
        {
            arrayEquipment[equipmentIndex].AddHeldCount();
        }

        arrayEquipment[equipmentIndex].UpdateHeldCount();
        arrayEquipment[equipmentIndex].UpdateHeldSliderValue();
    }

    public void UpdateCountwithIndex(int index, int count = 1)
    {
        if(index >= weaponChildCount)
        {
            Debug.Log("범위를 넘어섬");
            return;
        }

        if (!arrayEquipment[index].CheckUnlocked())
        {
            arrayEquipment[index].UnLockUpdate(true, true);
        }

        else
        {
            arrayEquipment[index].AddHeldCount(count);
            arrayEquipment[index].UpdateHeldCount();
            arrayEquipment[index].UpdateHeldSliderValue();
        }
    }

    private int FindEquipmentIndex(int itemCode)
    {
        for(int i = 0; i < weaponChildCount; i++)
        {
            if(arrayEquipment[i].GetItemCode() == itemCode)
            {
                return arrayEquipment[i].equipIndex;
            }
        }

        return 0;
    }

    public void ChangeMountedEquipment(int equipmentIndex)
    {
        arrayEquipment[mountedIndex].OnMount(false);
        mountedIndex = equipmentIndex;
        arrayEquipment[mountedIndex].OnMount(true);
    }

    public void DetailPopUp(int equipIndex)
    {
        PopUpManager.instance.AddPopUp(this.GetComponent<PopUpUI>());
        PopUpManager.instance.AddPopUp(weaponDetailPopUp, true);
        weaponDetail.UpdateDetailInfo(ref arrayEquipment[equipIndex]);
    }

    private void StartSetUp()
    {
        for (int i = 0; i < weaponChildCount; i++)
        {
            arrayEquipment[i].SetStatsFromDataBase();
        }

        arrayEquipment[0].OnMount(true);
        arrayEquipment[0].UnLockUpdate(true, true);

        int loopCount = (int)WeaponStatType.WeaponStatType_End;
        for (int i = 0; i < loopCount; i++)
        {
            UpdateWeaponStatsToPlayer((WeaponStatType)i);
        }
    }

    private void Start()
    {
        StartSetUp();
        gameObject.SetActive(false);
    }

    private void AwakeSetUp()
    {
        allWeaponsStats = new WeaponStatsForLocal();

        mountedIndex = 0;
        GameObject equip = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        weaponChildCount = equip.transform.childCount;
        arrayEquipment = new Weapon[weaponChildCount];
        for (int i = 0; i < weaponChildCount; i++)
        {
            arrayEquipment[i] = equip.transform.GetChild(i).GetComponent<Weapon>();
            arrayEquipment[i].equipIndex = i;
        }

        weaponDetail = transform.parent.GetChild(6).GetComponent<WeaponDetailInfo>();
        weaponDetailPopUp = transform.parent.GetChild(6).GetComponent<PopUpUI>();
    }

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
        AwakeSetUp();
    }
}
