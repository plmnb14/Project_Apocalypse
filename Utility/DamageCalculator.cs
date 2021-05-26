using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    const float calibrationPlusValue = 100.0f;
    const float calibrationMinusValue = 0.01f;
    static public long DamageCaculating(ref LivingData data, out bool isCritical)
    {
        long finalDamage = data.damageFinal;
        isCritical = false;

        if (ChanceCaculating(data.criticalChance * calibrationMinusValue))
        {
            finalDamage = (long)(finalDamage * (data.criticalDamage * calibrationMinusValue));
            isCritical = true;
        }

        return finalDamage;
    }

    static public long DefenseCaculating(ref LivingData data, long damage)
    {
        long finalDamage = data.damageFinal;
        if (ChanceCaculating(data.dodge * calibrationPlusValue))
        {
            finalDamage = 0;
        }

        finalDamage = (long)(finalDamage * DefenseReduce(data.armor));

        return finalDamage;
    }

    const int armorReduceValue = 5000;
     static public float DefenseReduce(int armorValue)
    {
        return armorValue / (armorValue + armorReduceValue);
    }

    const int randomValue = 10000000;
    const float minChanceValue = 0.000001f;
    static public bool ChanceCaculating(float chanceValue)
    {
        if (chanceValue < minChanceValue) chanceValue = minChanceValue;
        return Random.Range(0, randomValue) <= (chanceValue * randomValue) ? true : false;
    }
}
