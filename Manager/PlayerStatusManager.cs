using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroStatsEnum
{
    DamageFinal, AttackSpeed, HitPoint, Critical , CriticalDamage, Armor,
    MoreDamage, MinDamage, Pierce, HPRegen, Dodge,
    BuffDuration, DebuffResist, DropItem, GainGold, GainExp, DamageFixed, DamagePercent, Stats_End
};

public class HiddenStatus
{
    public float moreDmgChance;
    public float moreProjChance;
    public float moreAtkChance;
    public float igniteChance;
    public float stunChance;
    public float shockChance;
     
    public float moreDmgValue;
    public float moreProjValue;
    public float moreAtkValue;
    public float igniteValue;
    public float stunValue;
    public float shockValue;
}

public class PlayerStatusManager : MonoBehaviour
{
    static public PlayerStatusManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<PlayerStatusManager>();

            return m_instance;
        }
    }
    private static PlayerStatusManager m_instance;

    #region 플레이어 스탯정보 모음
    public HeroData baseHeroStats;
    public HeroData goldHeroStats;
    public HeroData cashHeroStats;
    public HeroData weaponHeroStats;
    public HeroData finalHeroStats;
    #endregion

    public HeroInfoMenuUI heroInfoMenuUI;

    private void CalculatePlayerDamage()
    {
        long finalDamage = (long)Mathf.Round(finalHeroStats.damageFixed * (1 + finalHeroStats.damagePercent * 0.01f));
        finalHeroStats.damageFinal = finalDamage;
    }

    public void UpdateValue(HeroStatsEnum stat, double value)
    {
        double finalValue = 0;
        switch(stat)
        {
            case HeroStatsEnum.DamageFinal:
                {
                    CalculatePlayerDamage();
                    finalValue = (double)finalHeroStats.damageFinal;
                    break;
                }
            case HeroStatsEnum.DamageFixed:
                {
                    goldHeroStats.damageFixed = (long)value;
                    finalHeroStats.damageFixed = baseHeroStats.damageFixed + goldHeroStats.damageFixed + weaponHeroStats.damageFixed;
                    CalculatePlayerDamage();
                    stat = HeroStatsEnum.DamageFinal;
                    finalValue = (double)finalHeroStats.damageFinal;
                    break;
                }
            case HeroStatsEnum.DamagePercent:
                {
                    goldHeroStats.damagePercent = (float)value;
                    finalHeroStats.damagePercent = baseHeroStats.damagePercent + goldHeroStats.damagePercent + weaponHeroStats.damagePercent;
                    CalculatePlayerDamage();
                    stat = HeroStatsEnum.DamageFinal;
                    finalValue = (double)finalHeroStats.damageFinal;
                    break;
                }
            case HeroStatsEnum.AttackSpeed:
                {
                    goldHeroStats.attackSpeed = (float)value;
                    finalHeroStats.attackSpeed = baseHeroStats.attackSpeed + goldHeroStats.attackSpeed + weaponHeroStats.attackSpeed;
                    finalValue = (double)finalHeroStats.attackSpeed;
                    StageManager.instance.hero.UpdateAttackSpeed(finalHeroStats.attackSpeed);
                    break;
                }
            case HeroStatsEnum.HitPoint:
                {
                    goldHeroStats.hitPoint = (int)value;
                    finalHeroStats.hitPoint = baseHeroStats.hitPoint + goldHeroStats.hitPoint + weaponHeroStats.hitPoint;
                    finalValue = (double)finalHeroStats.attackSpeed;
                    break;
                }
            case HeroStatsEnum.Critical:
                {
                    goldHeroStats.criticalChance = (float)value;
                    finalHeroStats.criticalChance = baseHeroStats.criticalChance + goldHeroStats.criticalChance + weaponHeroStats.criticalChance;
                    finalValue = (double)finalHeroStats.criticalChance;
                    break;
                }
            case HeroStatsEnum.CriticalDamage:
                {
                    goldHeroStats.criticalDamage = (float)value;
                    finalHeroStats.criticalDamage = baseHeroStats.criticalDamage + goldHeroStats.criticalDamage + +weaponHeroStats.criticalDamage;
                    finalValue = (double)finalHeroStats.criticalDamage;
                    break;
                }
            case HeroStatsEnum.Armor:
                {
                    goldHeroStats.armor = (int)value;
                    finalHeroStats.armor = baseHeroStats.armor + goldHeroStats.armor + +weaponHeroStats.armor;
                    finalValue = (double)finalHeroStats.armor;
                    break;
                }
            case HeroStatsEnum.MoreDamage:
                {
                    goldHeroStats.moreDamage = (int)value;
                    finalHeroStats.moreDamage = baseHeroStats.moreDamage + goldHeroStats.moreDamage + +weaponHeroStats.moreDamage;
                    finalValue = (double)finalHeroStats.moreDamage;
                    break;
                }
            case HeroStatsEnum.MinDamage:
                {
                    goldHeroStats.minDamage = (float)value;
                    finalHeroStats.minDamage = baseHeroStats.minDamage + goldHeroStats.minDamage + +weaponHeroStats.minDamage;
                    finalValue = (double)finalHeroStats.minDamage;
                    break;
                }
            case HeroStatsEnum.Pierce:
                {
                    goldHeroStats.armorPierce = (int)value;
                    finalHeroStats.armorPierce = baseHeroStats.armorPierce + goldHeroStats.armorPierce + +weaponHeroStats.armorPierce;
                    finalValue = (double)finalHeroStats.armorPierce;
                    break;
                }
            case HeroStatsEnum.HPRegen:
                {
                    goldHeroStats.hitPointRegen = (float)value;
                    finalHeroStats.hitPointRegen = baseHeroStats.hitPointRegen + goldHeroStats.hitPointRegen + weaponHeroStats.hitPointRegen;
                    finalValue = (double)finalHeroStats.hitPointRegen;
                    break;
                }
            case HeroStatsEnum.Dodge:
                {
                    goldHeroStats.dodge = (float)value;
                    finalHeroStats.dodge = baseHeroStats.dodge + goldHeroStats.dodge + weaponHeroStats.dodge;
                    finalValue = (double)finalHeroStats.attackSpeed;
                    break;
                }
            case HeroStatsEnum.BuffDuration:
                {
                    goldHeroStats.buffDuration = (float)value;
                    finalHeroStats.buffDuration = baseHeroStats.buffDuration + goldHeroStats.buffDuration + weaponHeroStats.buffDuration;
                    finalValue = (double)finalHeroStats.buffDuration;
                    break;
                }
            case HeroStatsEnum.DebuffResist:
                {
                    goldHeroStats.debuffResist = (float)value;
                    finalHeroStats.debuffResist = baseHeroStats.debuffResist + goldHeroStats.debuffResist + +weaponHeroStats.debuffResist;
                    finalValue = (double)finalHeroStats.debuffResist;
                    break;
                }
            case HeroStatsEnum.DropItem:
                {
                    goldHeroStats.dropItem = (int)value;
                    finalHeroStats.dropItem = baseHeroStats.dropItem + goldHeroStats.dropItem + +weaponHeroStats.dropItem;
                    finalValue = (double)finalHeroStats.dropItem;
                    break;
                }
            case HeroStatsEnum.GainGold:
                {
                    goldHeroStats.gainGold = (int)value;
                    finalHeroStats.gainGold = baseHeroStats.gainGold + goldHeroStats.gainGold + weaponHeroStats.gainGold;
                    finalValue = (double)finalHeroStats.gainGold;
                    break;
                }
            case HeroStatsEnum.GainExp:
                {
                    goldHeroStats.gainExp = (int)value;
                    finalHeroStats.gainExp = baseHeroStats.gainExp + goldHeroStats.gainExp + +weaponHeroStats.gainExp;
                    finalValue = (double)finalHeroStats.EXP;
                    break;
                }
        }

        heroInfoMenuUI.UpdateValue(stat, finalValue);
    }

    private void SetDefault()
    {
        baseHeroStats.objectName = "baseHeroStats";
        baseHeroStats.level = 1;
        baseHeroStats.EXP = 0;
        baseHeroStats.damageFixed = 100;
        baseHeroStats.damagePercent = 0.0f;
        baseHeroStats.hitPoint = 1000;
        baseHeroStats.armor = 0;
        baseHeroStats.attackSpeed = 1.0f;
        baseHeroStats.hitPointRegen = 0.01f;

        baseHeroStats.dodge = 0.05f;
        baseHeroStats.dropItem = 0;
        baseHeroStats.gainExp = 100;
        baseHeroStats.gainGold = 100;
        baseHeroStats.criticalChance = 10.0f;
        baseHeroStats.armorPierce = 0.0f;
        baseHeroStats.buffDuration = 1.0f;
        baseHeroStats.debuffResist = 0.0f;
        baseHeroStats.criticalDamage = 530.0f;
        baseHeroStats.minDamage = 0.7f;
        baseHeroStats.moreDamage = 0;

        goldHeroStats.objectName = "goldHeroStats";
        cashHeroStats.objectName = "cashHeroStats";
        finalHeroStats.objectName = "finalHeroStats";
        weaponHeroStats.objectName = "weaponHeroStats";
        finalHeroStats.AddData(ref baseHeroStats);
        finalHeroStats.AddData(ref goldHeroStats);
        finalHeroStats.AddData(ref cashHeroStats);
        finalHeroStats.AddData(ref weaponHeroStats);
    }

    private void SetUp()
    {
        goldHeroStats = new HeroData();
        baseHeroStats = new HeroData();
        finalHeroStats = new HeroData();
        cashHeroStats = new HeroData();
        weaponHeroStats = new HeroData();

        SetDefault();
    }

    private void Start()
    {
        heroInfoMenuUI.BeginSetUp(ref finalHeroStats);
    }

    private void Awake()
    {
        SetUp();
    }
}
