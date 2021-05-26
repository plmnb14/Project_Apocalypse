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
        int count = DataManger.instance.weaponEnchantTableList.Count;
        for (int i = 0; i < count; i++)
        {
            if (randomNumber <= DataManger.instance.weaponEnchantTableList[i].accumulateChance)
            {
                grade = DataManger.instance.weaponEnchantTableList[i].grade;
                break;
            }
        }
        return grade;
    }

    private static float PeekStat(int grade, EnchantStat randomStat) => randomStat switch
    {
        EnchantStat.DamageFixed => DataManger.instance.weaponEnchantTableList[grade].damageFixed,
        EnchantStat.DamagePercent => DataManger.instance.weaponEnchantTableList[grade].damagePercent,
        EnchantStat.CriticalChance => DataManger.instance.weaponEnchantTableList[grade].criticalChance,
        EnchantStat.CriticalDamage => DataManger.instance.weaponEnchantTableList[grade].criticalDamage,
        EnchantStat.HitPoint => DataManger.instance.weaponEnchantTableList[grade].hitPoint,
        EnchantStat.Armor => DataManger.instance.weaponEnchantTableList[grade].armor,
        _ => 0.0f
    };
}
