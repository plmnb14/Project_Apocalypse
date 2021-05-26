using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStatusUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public HeroStatsEnum statEnum;
    public double baseValue;

    private float goldIncreaseValue;
    private double reinforceValue;
    int reinforceMultiple;
    int reinforceLevel;
    long goldValue;

    StringBuilder stringBuilder;
    Text goldText;
    Text levelText;
    Text newLevelText;
    Text reinforveValueText;

    private const float baseWaitTime = 0.5f;
    private bool upgradeInfinite = false;
    private bool infinite = false;
    private float waitTime = baseWaitTime;
    private WaitForSeconds waitDown = new WaitForSeconds(0.05f);
    private IEnumerator CheckTime(PointerEventData eventData)
    {
        upgradeInfinite = true;

        while(waitTime > 0.0f)
        {
            if (!upgradeInfinite) yield break;

            waitTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        infinite = true;

        while (upgradeInfinite)
        {
            if(!Upgrade(eventData)) yield break;
            yield return waitDown;
        }

        infinite = false;
        waitTime = baseWaitTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(CheckTime(eventData));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!infinite) Upgrade(eventData);

        upgradeInfinite = false;
    }

    public bool Upgrade(PointerEventData eventdata)
    {
        if(ResourceManager.instance.Coin >= goldValue)
        {
            ResourceManager.instance.Coin -= goldValue;

            long upgradeValue = (long)(goldValue * goldIncreaseValue);

            goldValue = upgradeValue > goldValue ? upgradeValue : upgradeValue + 1;
            reinforceLevel += reinforceMultiple;
            reinforceValue = baseValue * reinforceLevel;
            PlayerStatusManager.instance.UpdateValue(statEnum, reinforceValue);

            goldText.text = NumToString.GetNumberString(ref stringBuilder, upgradeValue, NumToString.buildSetting.GLOBAL);
            levelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, NumToString.buildSetting.GLOBAL);
            newLevelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, "lv.", NumToString.buildSetting.GLOBAL);

            reinforveValueText.text = reinforceValue < 1000.0 ?
                "+" + reinforceValue.ToString() : NumToString.GetNumberString(ref stringBuilder, (long)reinforceValue, "+", NumToString.buildSetting.GLOBAL);

            return true;
        }

        return false;
    }

    private void SetUp()
    {
        goldIncreaseValue = 1.07f;
        goldValue = 100;
        reinforceLevel = 0;
        reinforceMultiple = 1;

        stringBuilder = new StringBuilder(20, 20);

        goldText = transform.GetChild(1).GetComponent<Text>();
        levelText = transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();
        newLevelText = transform.parent.GetChild(5).GetComponent<Text>();
        reinforveValueText = transform.parent.GetChild(4).GetComponent<Text>();
    }

    private void CaculatingGoldIncrease()
    {
        switch (statEnum)
        {
            case HeroStatsEnum.DamageFixed:
            case HeroStatsEnum.Armor:
            case HeroStatsEnum.HitPoint:
                {
                    goldIncreaseValue = 1.03f;
                    break;
                }
            default:
                {
                    goldIncreaseValue = 1.07f;
                    break;
                }
        }

    }

    private void Start()
    {
        levelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, NumToString.buildSetting.GLOBAL);
        goldText.text = NumToString.GetNumberString(ref stringBuilder, goldValue, NumToString.buildSetting.GLOBAL);
        newLevelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, "lv.", NumToString.buildSetting.GLOBAL);
        
        reinforveValueText.text = reinforceValue < 1000.0 ? 
            "+" + reinforceValue.ToString() : NumToString.GetNumberString(ref stringBuilder, (long)reinforceValue, "+", NumToString.buildSetting.GLOBAL);
    }

    private void Awake()
    {
        SetUp();
    }
}
