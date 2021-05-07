using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    HeroData baseHeroStatus { get; set; }
    HeroData goldHeroStatus { get; set; }
    HeroData cashHeroStatus { get; set; }
    HeroData finalHeroStatus { get; set; }

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
    }

    private void SetUp()
    {
        goldHeroStatus = new HeroData();
        baseHeroStatus = new HeroData();
        SetDefault();
    }

    private void Awake()
    {
        SetUp();   
    }
}
