using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoMenuUI : ContentsMenu
{
    private Text[] statusText;
    private StringBuilder valueString;

    public void UpdateValue(HeroStatsEnum idx, double value)
    {
        valueString.Remove(0, valueString.Length);

        switch (idx)
        {
            case HeroStatsEnum.DamageFinal:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.AttackSpeed:
                {
                    valueString.Append(value.ToString("N3"));
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.HitPoint:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.Critical:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.CriticalDamage:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.Armor:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.MoreDamage:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.MinDamage:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.Pierce:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.HPRegen:
                {
                    valueString.Append(value.ToString("N3"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.Dodge:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.BuffDuration:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.DebuffResist:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.DropItem:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.GainGold:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatsEnum.GainExp:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void BeginSetUp(ref HeroData data)
    {
        UpdateValue(HeroStatsEnum.DamageFinal, data.damageFinal);
        UpdateValue(HeroStatsEnum.AttackSpeed, data.attackSpeed);
        UpdateValue(HeroStatsEnum.HitPoint, data.hitPoint);
        UpdateValue(HeroStatsEnum.Critical, data.criticalChance);
        UpdateValue(HeroStatsEnum.CriticalDamage, data.criticalDamage);
        UpdateValue(HeroStatsEnum.Armor, data.armor);

        UpdateValue(HeroStatsEnum.MoreDamage, data.moreDamage);
        UpdateValue(HeroStatsEnum.MinDamage, data.minDamage);
        UpdateValue(HeroStatsEnum.Pierce, data.armorPierce);
        UpdateValue(HeroStatsEnum.HPRegen, data.hitPointRegen);
        UpdateValue(HeroStatsEnum.Dodge, data.dodge);

        UpdateValue(HeroStatsEnum.BuffDuration, data.buffDuration);
        UpdateValue(HeroStatsEnum.DebuffResist, data.debuffResist);
        UpdateValue(HeroStatsEnum.DropItem, data.dropItem);
        UpdateValue(HeroStatsEnum.GainGold, data.gainGold);
        UpdateValue(HeroStatsEnum.GainExp, data.gainExp);
    }

    private void SetUp()
    {
        valueString = new StringBuilder(20,20);
        statusText = new Text[(int)HeroStatsEnum.Stats_End];

        Transform targetTrans = transform.GetChild(1).transform.GetChild(1).gameObject.transform;
        for(int i  = 0; i < 6; i++)
        {
            statusText[i] = targetTrans.GetChild(i).GetComponent<Text>();
        }

        targetTrans = transform.GetChild(2).transform.GetChild(1).gameObject.transform;
        for (int i = 0; i < 5; i++)
        {
            statusText[6+i] = targetTrans.GetChild(i).GetComponent<Text>();
        }

        targetTrans = transform.GetChild(3).transform.GetChild(1).gameObject.transform;
        for (int i = 0; i < 5; i++)
        {
            statusText[11 + i] = targetTrans.GetChild(i).GetComponent<Text>();
        }
    }

    private void Awake()
    {
        SetUp();
    }
}
