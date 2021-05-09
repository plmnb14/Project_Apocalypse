using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStatusUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    int reinforceMultiple;
    int reinforceLevel;
    long goldValue;
    float reinforceValue;
    float baseValue;

    StringBuilder stringBuilder;
    Text goldText;
    Text levelText;

    private bool upgradeInfinite = false;
    private bool infinite = false;
    private float waitTime = 1.0f;
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
        waitTime = 1.0f;
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

            long upgradeValue = (long)(goldValue * 1.07f);

            goldValue = upgradeValue > goldValue ? upgradeValue : upgradeValue + 1;
            reinforceLevel += reinforceMultiple;

            goldText.text = NumToString.GetNumberString(ref stringBuilder, upgradeValue, NumToString.buildSetting.GLOBAL);
            levelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, NumToString.buildSetting.GLOBAL);

            reinforceValue = baseValue * reinforceLevel;
            PlayerStatusManager.instance.UpdateValue(StatusIndex.DMG, (long)reinforceValue);

            return true;
        }

        return false;
    }

    private void SetUp()
    {
        goldValue = 100;
        reinforceLevel = 0;
        reinforceValue = 0.0f;
        reinforceMultiple = 1;
        baseValue = 50.0f;

        stringBuilder = new StringBuilder(20, 20);

        goldText = transform.GetChild(1).GetComponent<Text>();
        levelText = transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        levelText.text = NumToString.GetNumberString(ref stringBuilder, reinforceLevel, NumToString.buildSetting.GLOBAL);
        goldText.text = NumToString.GetNumberString(ref stringBuilder, goldValue, NumToString.buildSetting.GLOBAL);
    }

    private void Awake()
    {
        SetUp();
    }
}
