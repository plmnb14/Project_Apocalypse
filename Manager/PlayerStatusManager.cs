using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroStatusEnum
{
    Damage, AttackSpeed, HitPoint, Critical , CriticalDamage, Armor,
    MoreDamage, MinDamage, Pierce, HPRegen, Dodge,
    BuffDuration, DebuffResist, DropItem, GainGold, GainExp, Status_End
};

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
    public HeroData baseHeroStatus;
    public HeroData goldHeroStatus;
    public HeroData cashHeroStatus;
    public HeroData finalHeroStatus;
    #endregion

    private UI_HeroInfo heroInfo;

    public void UpdateValue(HeroStatusEnum stat, double value)
    {
        switch(stat)
        {
            case HeroStatusEnum.Damage :
                {
                    goldHeroStatus.damage = (long)value;
                    finalHeroStatus.damage = baseHeroStatus.damage + goldHeroStatus.damage;

                    heroInfo.UpdateValue(stat, finalHeroStatus.damage);
                    break;
                }
            case HeroStatusEnum.AttackSpeed:
                {
                    goldHeroStatus.attackSpeed = (float)value;
                    finalHeroStatus.attackSpeed = baseHeroStatus.attackSpeed + goldHeroStatus.attackSpeed;

                    heroInfo.UpdateValue(stat, finalHeroStatus.attackSpeed);
                    StageManager.instance.hero.UpdateAttackSpeed(finalHeroStatus.attackSpeed);
                    break;
                }
            case HeroStatusEnum.HitPoint:
                {
                    goldHeroStatus.hitPoint = (int)value;
                    finalHeroStatus.hitPoint = baseHeroStatus.hitPoint + goldHeroStatus.hitPoint;

                    heroInfo.UpdateValue(stat, finalHeroStatus.hitPoint);
                    break;
                }
            case HeroStatusEnum.Critical:
                {
                    goldHeroStatus.criticalChance = (float)value;
                    finalHeroStatus.criticalChance = baseHeroStatus.criticalChance + goldHeroStatus.criticalChance;

                    heroInfo.UpdateValue(stat, finalHeroStatus.criticalChance);
                    break;
                }
            case HeroStatusEnum.CriticalDamage:
                {
                    goldHeroStatus.criticalDamage = (float)value;
                    finalHeroStatus.criticalDamage = baseHeroStatus.criticalDamage + goldHeroStatus.criticalDamage;

                    heroInfo.UpdateValue(stat, finalHeroStatus.criticalDamage);
                    break;
                }
            case HeroStatusEnum.Armor:
                {
                    goldHeroStatus.armor = (int)value;
                    finalHeroStatus.armor = baseHeroStatus.armor + goldHeroStatus.armor;

                    heroInfo.UpdateValue(stat, finalHeroStatus.armor);
                    break;
                }
            case HeroStatusEnum.MoreDamage:
                {
                    goldHeroStatus.moreDamage = (int)value;
                    finalHeroStatus.moreDamage = baseHeroStatus.moreDamage + goldHeroStatus.moreDamage;

                    heroInfo.UpdateValue(stat, finalHeroStatus.moreDamage);
                    break;
                }
            case HeroStatusEnum.MinDamage:
                {
                    goldHeroStatus.minDamage = (float)value;
                    finalHeroStatus.minDamage = baseHeroStatus.minDamage + goldHeroStatus.minDamage;

                    heroInfo.UpdateValue(stat, finalHeroStatus.minDamage);
                    break;
                }
            case HeroStatusEnum.Pierce:
                {
                    goldHeroStatus.armorPierce = (int)value;
                    finalHeroStatus.armorPierce = baseHeroStatus.armorPierce + goldHeroStatus.armorPierce;

                    heroInfo.UpdateValue(stat, finalHeroStatus.armorPierce);
                    break;
                }
            case HeroStatusEnum.HPRegen:
                {
                    goldHeroStatus.hitPointRegen = (float)value;
                    finalHeroStatus.hitPointRegen = baseHeroStatus.hitPointRegen + goldHeroStatus.hitPointRegen;

                    heroInfo.UpdateValue(stat, finalHeroStatus.hitPointRegen);
                    break;
                }
            case HeroStatusEnum.Dodge:
                {
                    goldHeroStatus.dodge = (float)value;
                    finalHeroStatus.dodge = baseHeroStatus.dodge + goldHeroStatus.dodge;

                    heroInfo.UpdateValue(stat, finalHeroStatus.dodge);
                    break;
                }
            case HeroStatusEnum.BuffDuration:
                {
                    goldHeroStatus.buffDuration = (float)value;
                    finalHeroStatus.buffDuration = baseHeroStatus.buffDuration + goldHeroStatus.buffDuration;

                    heroInfo.UpdateValue(stat, finalHeroStatus.buffDuration);
                    break;
                }
            case HeroStatusEnum.DebuffResist:
                {
                    goldHeroStatus.debuffResist = (float)value;
                    finalHeroStatus.debuffResist = baseHeroStatus.debuffResist + goldHeroStatus.debuffResist;

                    heroInfo.UpdateValue(stat, finalHeroStatus.debuffResist);
                    break;
                }
            case HeroStatusEnum.DropItem:
                {
                    goldHeroStatus.dropItem = (int)value;
                    finalHeroStatus.dropItem = baseHeroStatus.dropItem + goldHeroStatus.dropItem;

                    heroInfo.UpdateValue(stat, finalHeroStatus.dropItem);
                    break;
                }
            case HeroStatusEnum.GainGold:
                {
                    goldHeroStatus.gainGold = (int)value;
                    finalHeroStatus.gainGold = baseHeroStatus.gainGold + goldHeroStatus.gainGold;

                    heroInfo.UpdateValue(stat, finalHeroStatus.gainGold);
                    break;
                }
            case HeroStatusEnum.GainExp:
                {
                    goldHeroStatus.gainExp = (int)value;
                    finalHeroStatus.gainExp = baseHeroStatus.gainExp + goldHeroStatus.gainExp;

                    heroInfo.UpdateValue(stat, finalHeroStatus.gainExp);
                    break;
                }
        }
    }

    private void SetDefault()
    {
        baseHeroStatus.objectName = "baseHeroStatus";
        baseHeroStatus.level = 1;
        baseHeroStatus.EXP = 0;
        baseHeroStatus.damage = 100;
        baseHeroStatus.hitPoint = 1000;
        baseHeroStatus.armor = 0;
        baseHeroStatus.attackSpeed = 1.0f;
        baseHeroStatus.hitPointRegen = 0.01f;

        baseHeroStatus.dodge = 0.05f;
        baseHeroStatus.dropItem = 0;
        baseHeroStatus.gainExp = 100;
        baseHeroStatus.gainGold = 100;
        baseHeroStatus.criticalChance = 10.0f;
        baseHeroStatus.armorPierce = 0.0f;
        baseHeroStatus.buffDuration = 1.0f;
        baseHeroStatus.debuffResist = 0.0f;
        baseHeroStatus.criticalDamage = 530.0f;
        baseHeroStatus.minDamage = 0.7f;
        baseHeroStatus.moreDamage = 0;

        goldHeroStatus.objectName = "goldHeroStatus";
        cashHeroStatus.objectName = "cashHeroStatus";
        finalHeroStatus.objectName = "finalHeroStatus";
        finalHeroStatus.AddData(ref baseHeroStatus);
        finalHeroStatus.AddData(ref goldHeroStatus);
        finalHeroStatus.AddData(ref cashHeroStatus);
    }

    private void SetUp()
    {
        goldHeroStatus = new HeroData();
        baseHeroStatus = new HeroData();
        finalHeroStatus = new HeroData();
        cashHeroStatus = new HeroData();

        SetDefault();
    }

    private void Start()
    {
        heroInfo = GameObject.Find("Canvas_Info").GetComponent<UI_HeroInfo>();

        heroInfo.BeginSetUp(ref finalHeroStatus);
    }

    private void Awake()
    {
        SetUp();
    }
}
