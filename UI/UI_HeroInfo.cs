using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfo : MonoBehaviour
{
    private Text[] statusText;
    private StringBuilder valueString;

    public void UpdateValue(HeroStatusEnum idx, double value)
    {
        valueString.Remove(0, valueString.Length);

        switch (idx)
        {
            case HeroStatusEnum.Damage:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.AttackSpeed:
                {
                    valueString.Append(value.ToString("N3"));
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.HitPoint:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.Critical:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.CriticalDamage:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.Armor:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.MoreDamage:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.MinDamage:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.Pierce:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.HPRegen:
                {
                    valueString.Append(value.ToString("N3"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.Dodge:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.BuffDuration:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.DebuffResist:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.DropItem:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.GainGold:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case HeroStatusEnum.GainExp:
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
        UpdateValue(HeroStatusEnum.Damage, data.damage);
        UpdateValue(HeroStatusEnum.AttackSpeed, data.attackSpeed);
        UpdateValue(HeroStatusEnum.HitPoint, data.hitPoint);
        UpdateValue(HeroStatusEnum.Critical, data.criticalChance);
        UpdateValue(HeroStatusEnum.CriticalDamage, data.criticalDamage);
        UpdateValue(HeroStatusEnum.Armor, data.armor);

        UpdateValue(HeroStatusEnum.MoreDamage, data.moreDamage);
        UpdateValue(HeroStatusEnum.MinDamage, data.minDamage);
        UpdateValue(HeroStatusEnum.Pierce, data.armorPierce);
        UpdateValue(HeroStatusEnum.HPRegen, data.hitPointRegen);
        UpdateValue(HeroStatusEnum.Dodge, data.dodge);

        UpdateValue(HeroStatusEnum.BuffDuration, data.buffDuration);
        UpdateValue(HeroStatusEnum.DebuffResist, data.debuffResist);
        UpdateValue(HeroStatusEnum.DropItem, data.dropItem);
        UpdateValue(HeroStatusEnum.GainGold, data.gainGold);
        UpdateValue(HeroStatusEnum.GainExp, data.gainExp);
    }

    private void SetUp()
    {
        valueString = new StringBuilder(20,20);
        statusText = new Text[(int)HeroStatusEnum.Status_End];

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
