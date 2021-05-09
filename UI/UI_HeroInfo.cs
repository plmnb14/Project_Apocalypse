using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroInfo : MonoBehaviour
{
    private Text[] statusText;
    private StringBuilder valueString;

    public void UpdateValue(StatusIndex idx, double value)
    {
        valueString.Remove(0, valueString.Length);

        switch (idx)
        {
            case StatusIndex.DMG:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.ATKSPD:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.HP:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.CRI:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.CRIDMG:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.ARMOR:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append("    ");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.MORDMG:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.MINDMG:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.PIERCE:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N1"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.REGEN:
                {
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.DODGE:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.BUFF:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.RESIST:
                {
                    value *= 100.0f;
                    valueString.Append(value.ToString("N2"));
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.DROP:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.RESGAIN:
                {
                    NumToString.GetNumberString(
                        ref valueString, (long)value, NumToString.buildSetting.GLOBAL);
                    valueString.Append(" %");
                    statusText[(int)idx].text = valueString.ToString();
                    break;
                }
            case StatusIndex.EXPGAIN:
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

    public void BeginSetUp(HeroData data)
    {
        UpdateValue(StatusIndex.DMG, data.damage);
        UpdateValue(StatusIndex.ATKSPD, 1.0f);
        UpdateValue(StatusIndex.HP, 1000);
        UpdateValue(StatusIndex.CRI, 32.35f);
        UpdateValue(StatusIndex.CRIDMG, 150);
        UpdateValue(StatusIndex.ARMOR, 6);

        UpdateValue(StatusIndex.MORDMG, 1235);
        UpdateValue(StatusIndex.MINDMG, 0.75f);
        UpdateValue(StatusIndex.PIERCE, 0.3f);
        UpdateValue(StatusIndex.REGEN, 0.01f);
        UpdateValue(StatusIndex.DODGE, 0.03f);

        UpdateValue(StatusIndex.BUFF, 1.0f);
        UpdateValue(StatusIndex.RESIST, 0.56f);
        UpdateValue(StatusIndex.DROP, 25);
        UpdateValue(StatusIndex.RESGAIN, 120);
        UpdateValue(StatusIndex.EXPGAIN, 120);
    }

    private void SetUp()
    {
        valueString = new StringBuilder(20,20);
        statusText = new Text[(int)StatusIndex.STATUS_END];

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
