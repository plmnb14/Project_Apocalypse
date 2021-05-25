using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EnchantType { Damage, CriticalChance, CriticalDamage, EnchantType_End }
public enum HeldStatType { None = -1, DamageFixed, DamagePercent, CriticalChance, HeldType_End }

public enum WeaponStatType { DamageFixed, DamagePercent, CriticalChance, CriticalDamage, WeaponStatType_End };

public struct WeaponStatsForSave
{
    public int itemCode;
    public int reinforcelevel;
    public int heldCount;
    public EnchantType enchantType;
    public bool isMounted;
    public bool isUnlock;
}

public struct WeaponStatsForDatabase
{
    public int itemCode;
    public string weaponName;
    public int grade;
    public int tier;
    public int reinforceCostOrigin;
    public int damageFixedOrigin;
    public int criticalDamageOrigin;
    public float damagePercentOrigin;
    public float criticalChanceOrigin;
    public HeldStatType heldStatusType_0;
    public HeldStatType heldStatusType_1;
    public HeldStatType heldStatusType_2;
    public float heldStatusValueOrigin_0;
    public float heldStatusValueOrigin_1;
    public float heldStatusValueOrigin_2;
}

public struct WeaponStatsForLocal
{
    public int itemCode;
    public string weaponName;
    public int reinforceCost;
    public int damageFixed;
    public int criticalDamage;
    public float damagePercent;
    public float criticalChance;
    public float heldStatusValue_0;
    public float heldStatusValue_1;
    public float heldStatusValue_2;
    public float enchantValue;
}

public class Weapon : MonoBehaviour, IPointerClickHandler
{
    private const string suffix = " / 4";

    public WeaponStatsForSave statForSave;
    public WeaponStatsForLocal statForLocal;
    private WeaponManager weaponManager;

    public int equipIndex { get; set; }

    private StringBuilder countStringBuilder;
    private StringBuilder levelStringBuilder;
    private Slider equipmentSlider;
    private Text heldCountText;
    private Text levelText;
    private GameObject lockScreen;

    public Text equipmentName { get; set; }
    public Text tierText { get; set; }
    public Image backgroundImage { get; set; }
    public Image rarityIcon { get; set; }
    public Image equipmentIcon { get; set; }

    public void DeepCopy(ref Weapon weapon)
    {
        statForSave = weapon.statForSave;
        statForLocal = weapon.statForLocal;
        equipIndex = weapon.equipIndex;

        UpdateHeldCount();
        UpdateHeldSliderValue();
        UpdateLevel(true);
        UnLockUpdate(statForSave.isUnlock);

        backgroundImage.sprite = weapon.backgroundImage.sprite;
        rarityIcon.sprite = weapon.rarityIcon.sprite;
        equipmentIcon.sprite = weapon.equipmentIcon.sprite;
        tierText.text = weapon.tierText.text;
        equipmentName.text = weapon.equipmentName.text;
    }

    public bool CheckMounted() { return statForSave.isMounted; }
    public bool CheckUnlocked() { return statForSave.isUnlock; }
    public int GetItemCode() { return statForSave.itemCode; }
    public void SetItemCode(int itemCode) { statForSave.itemCode = itemCode; }

    public void SetStatsFromDataBase()
    {
        DataManger dataManager = DataManger.instance;
        equipmentName.text = dataManager.weaponStatForDataList[equipIndex].weaponName;
        statForSave.itemCode = statForLocal.itemCode = dataManager.weaponStatForDataList[equipIndex].itemCode;

        SetLocalStatFromDataBase();
    }

    public void OnMount(bool isMount)
    {
        statForSave.isMounted = isMount;
        SetLocalStatsToManager(isMount);
    }

    public void SetLocalStatsToManager(bool isMount)
    {
        weaponManager.allWeaponsStats.damageFixed += isMount ? statForLocal.damageFixed : -statForLocal.damageFixed;
        weaponManager.allWeaponsStats.damagePercent += isMount ? statForLocal.damagePercent : -statForLocal.damagePercent;
        weaponManager.allWeaponsStats.criticalChance += isMount ? statForLocal.criticalChance : -statForLocal.criticalChance;
        weaponManager.allWeaponsStats.criticalDamage += isMount ? statForLocal.criticalDamage : -statForLocal.criticalDamage;
    }

    private void SetLocalStatFromDataBase()
    {
        SetReinforceCost();
        SetDamageFixed();
        SetDamagePercent();
        SetCriticalChance();
        SetCriticalDamage();
        for (int i = 0; i < 3; i++) SetHeldStat(i);
    }

    #region Local Stats Update
    private void SetReinforceCost()
    {
        statForLocal.reinforceCost = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].reinforceCostOrigin * statForSave.reinforcelevel;
    }
    private void SetDamageFixed()
    {
        statForLocal.damageFixed = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].damageFixedOrigin * statForSave.reinforcelevel;
    }

    private void SetDamagePercent()
    {
        statForLocal.damagePercent = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].damagePercentOrigin * statForSave.reinforcelevel;
    }
    private void SetCriticalChance()
    {
        statForLocal.criticalChance = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].criticalChanceOrigin * statForSave.reinforcelevel;
    }

    private void SetCriticalDamage()
    {
        statForLocal.criticalDamage = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].criticalDamageOrigin * statForSave.reinforcelevel;
    }

    private void SetHeldStat(int statIndex)
    {
        switch(statIndex)
        {
            case 0:
                {
                    statForLocal.heldStatusValue_0 = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].heldStatusValueOrigin_0 * statForSave.reinforcelevel;
                    break;
                }
            case 1:
                {
                    statForLocal.heldStatusValue_1 = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].heldStatusValueOrigin_1 * statForSave.reinforcelevel;
                    break;
                }
            case 2:
                {
                    statForLocal.heldStatusValue_2 = DataManger.instance.weaponStatForDataDictionary[statForSave.itemCode].heldStatusValueOrigin_2 * statForSave.reinforcelevel;
                    break;
                }
        }
    }
    #endregion

    public void OnMount()
    {
        statForSave.isMounted = true;
        weaponManager.damage += statForSave.reinforcelevel; // 여기 DB 공격력 넣어줘야함.
        // 여기 공격력 이외에 나머지 능력치
        weaponManager.UpdateDamage();
    }

    public void OnDismount()
    {
        statForSave.isMounted = false;
        weaponManager.damage -= statForSave.reinforcelevel;
        weaponManager.UpdateDamage();
    }

    public bool OnReinforce()
    {
        bool isReinforceSucceed = false;
        if(statForLocal.reinforceCost >= ResourceManager.instance.Coin)
        {
            UpdateLevel();
            isReinforceSucceed = true;
        }

        return isReinforceSucceed;
    }

    public int OnPromote()
    {
        int promoteCount = statForSave.heldCount / 4;

        if(promoteCount > 0)
        {
            statForSave.heldCount -= promoteCount * 4;

            UpdateHeldCount();
            UpdateHeldSliderValue();
        }

        return promoteCount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        weaponManager.DetailPopUp(equipIndex);
    }

    public void UnLockUpdate(bool value)
    {
        statForSave.isUnlock = value;

        levelText.gameObject.SetActive(statForSave.isUnlock);
        equipmentSlider.gameObject.SetActive(statForSave.isUnlock);
        equipmentName.gameObject.SetActive(statForSave.isUnlock);

        lockScreen.gameObject.SetActive(!statForSave.isUnlock);

        if(value)
        {
            UpdateDamage(true);
        }
    }

    public void AddHeldCount(int addCount = 1)
    {
        statForSave.heldCount += addCount;
    }

    public void SetHeldCount(int setCount)
    {
        statForSave.heldCount = setCount;
    }

    public void UpdateHeldCount()
    {
        countStringBuilder.Remove(0, countStringBuilder.Length);
        countStringBuilder.Append(statForSave.heldCount);
        countStringBuilder.Append(suffix);
        heldCountText.text = countStringBuilder.ToString();
    }

    public void UpdateLevel(bool onlyUpdate = false, int value = 1)
    {
        if(!onlyUpdate)
        {
            statForSave.reinforcelevel += value;
            UpdateDamage();
        }

        levelStringBuilder.Remove(0, levelStringBuilder.Length);
        levelStringBuilder.Append("+");
        levelStringBuilder.Append(statForSave.reinforcelevel);
        levelText.text = levelStringBuilder.ToString();
    }

    private void UpdateDamagePercent(bool isNew = false)
    {
        if (!isNew) weaponManager.allWeaponsStats.damagePercent -= statForLocal.damagePercent;
        SetDamagePercent();
        weaponManager.UpdateWeaponStats(WeaponStatType.DamagePercent);
    }

    private void UpdateDamageFixed(bool isNew = false)
    {
        if(!isNew) weaponManager.allWeaponsStats.damageFixed -= statForLocal.damageFixed;
        SetDamageFixed();
        weaponManager.UpdateWeaponStats(WeaponStatType.DamageFixed);
    }

    public void UpdateHeldSliderValue()
    {
        equipmentSlider.value = statForSave.heldCount;
    }

    private void GetChild()
    {
        equipmentSlider = transform.GetChild(2).GetComponent<Slider>();
        heldCountText = equipmentSlider.transform.GetChild(4).GetComponent<Text>();
        tierText = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        levelText = transform.GetChild(0).GetChild(2).GetComponent<Text>();
        equipmentName = transform.GetChild(3).GetComponent<Text>();
        lockScreen = transform.GetChild(4).gameObject;
    }

    private void InitializeMember()
    {
        statForSave = new WeaponStatsForSave();
        statForSave.reinforcelevel = 1;
        statForLocal = new WeaponStatsForLocal();

        countStringBuilder = new StringBuilder(10, 10);
        levelStringBuilder = new StringBuilder(10, 10);

        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        rarityIcon = transform.GetChild(1).GetComponent<Image>();
        equipmentIcon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void AwakeSetUp()
    {
        weaponManager = WeaponManager.instance;
        InitializeMember();
        GetChild();
        UpdateHeldCount();
        UpdateHeldSliderValue();
        UpdateLevel(true);
        UnLockUpdate(false);
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
