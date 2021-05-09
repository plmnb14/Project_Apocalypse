using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusIndex : int
{
    DMG, ATKSPD, HP, CRI, CRIDMG, ARMOR,
    MORDMG, MINDMG, PIERCE, REGEN, DODGE,
    BUFF, RESIST, DROP, RESGAIN, EXPGAIN, STATUS_END
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
    public HeroData baseHeroStatus { get; set; }
    public HeroData goldHeroStatus { get; set; }
    public HeroData cashHeroStatus { get; set; }
    public HeroData finalHeroStatus { get; set; }
    #endregion

    private UI_HeroInfo heroInfo;

    public void UpdateValue(StatusIndex idx, double value)
    {
        goldHeroStatus.damage = (long)value;
        finalHeroStatus.damage = baseHeroStatus.damage + goldHeroStatus.damage;

        heroInfo.UpdateValue(idx, finalHeroStatus.damage);
    }

    private void SetDefault()
    {
        goldHeroStatus.objectName = "goldHeroStatus";
        goldHeroStatus.level = 0;
        goldHeroStatus.EXP = 0;
        goldHeroStatus.damage = 0;
        goldHeroStatus.hitPoint = 0;
        goldHeroStatus.armor = 0;
        goldHeroStatus.attackSpeed = 0.0f;
        goldHeroStatus.hitPointRegen = 0.0f;

        goldHeroStatus.dodge = 0.0f;
        goldHeroStatus.gainItem = 0;
        goldHeroStatus.gainEXP = 0;
        goldHeroStatus.gainGold = 0;
        goldHeroStatus.criticalChance = 0.0f;
        goldHeroStatus.armorPierce = 0.0f;
        goldHeroStatus.buffDuration = 0.0f;
        goldHeroStatus.debuffResist = 0.0f;
        goldHeroStatus.criticalDamage = 0.0f;
        goldHeroStatus.moreDamage = 0;


        baseHeroStatus.objectName = "baseHeroStatus";
        baseHeroStatus.level = 1;
        baseHeroStatus.EXP = 0;
        baseHeroStatus.damage = 100;
        baseHeroStatus.hitPoint = 1000;
        baseHeroStatus.armor = 0;
        baseHeroStatus.attackSpeed = 1.0f;
        baseHeroStatus.hitPointRegen = 0.01f;

        baseHeroStatus.dodge = 0.05f;
        baseHeroStatus.gainItem = 0;
        baseHeroStatus.gainEXP = 100;
        baseHeroStatus.gainGold = 100;
        baseHeroStatus.criticalChance = 0.05f;
        baseHeroStatus.armorPierce = 0.0f;
        baseHeroStatus.buffDuration = 1.0f;
        baseHeroStatus.debuffResist = 0.0f;
        baseHeroStatus.criticalDamage = 1.25f;
        baseHeroStatus.moreDamage = 0;

        finalHeroStatus.damage = baseHeroStatus.damage + goldHeroStatus.damage;
    }

    private void SetUp()
    {
        goldHeroStatus = new HeroData();
        baseHeroStatus = new HeroData();
        finalHeroStatus = new HeroData();

        SetDefault();
    }

    private void Start()
    {
        heroInfo = GameObject.Find("Canvas_Info").GetComponent<UI_HeroInfo>();

        heroInfo.BeginSetUp(finalHeroStatus);
    }

    private void Awake()
    {
        SetUp();
    }
}
