using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponEnchantTable
{
    public int grade;
    public float chance;
    public int accumulateChance;
    public int damageFixed;
    public float damagePercent;
    public float criticalChance;
    public float criticalDamage;
    public float hitPoint;
    public float armor;
}

public enum EnchantStat 
{ 
    DamageFixed, 
    DamagePercent, 
    CriticalChance, 
    CriticalDamage,
    HitPoint, 
    Armor, 
    EnchantStat_End 
};

public class WeaponEnchant : MonoBehaviour
{
    public static void EnchantWeapon(ref Weapon weapon)
    {
        int randomGrade = PeekGrade();
        EnchantStat randomStat = (EnchantStat)Random.Range(0, (int)EnchantStat.EnchantStat_End);
        weapon.statForSave.enchantGrade = randomGrade;
        weapon.statForSave.enchantType = randomStat;
        weapon.statForLocal.enchantValue = PeekStat(5 - randomGrade, randomStat);
    }

    private static int PeekGrade()
    {
        int randomNumber = Random.Range(0, 100000000);
        int grade = 0;
        int count = DataManager.Instance.weaponEnchantTableList.Count;
        for (int i = 0; i < count; i++)
        {
            if (randomNumber <= DataManager.Instance.weaponEnchantTableList[i].accumulateChance)
            {
                grade = DataManager.Instance.weaponEnchantTableList[i].grade;
                break;
            }
        }
        return grade;
    }

    private static float PeekStat(int grade, EnchantStat randomStat) => randomStat switch
    {
        EnchantStat.DamageFixed => DataManager.Instance.weaponEnchantTableList[grade].damageFixed,
        EnchantStat.DamagePercent => DataManager.Instance.weaponEnchantTableList[grade].damagePercent,
        EnchantStat.CriticalChance => DataManager.Instance.weaponEnchantTableList[grade].criticalChance,
        EnchantStat.CriticalDamage => DataManager.Instance.weaponEnchantTableList[grade].criticalDamage,
        EnchantStat.HitPoint => DataManager.Instance.weaponEnchantTableList[grade].hitPoint,
        EnchantStat.Armor => DataManager.Instance.weaponEnchantTableList[grade].armor,
        _ => 0.0f
    };
}
