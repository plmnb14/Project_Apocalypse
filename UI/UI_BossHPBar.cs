using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHPBar : UI_HPBar
{
    UI_FadeInOut fadeInOut;
    CanvasGroup canvasGroup;

    protected override void OnDie()
    {
        StartCoroutine(fadeInOut.FadeOut(canvasGroup, ()=>gameObject.SetActive(false)));

        base.OnDie();
    }

    protected override void SetUp()
    {
        base.SetUp();

        HpSlider = rectTrans.GetChild(1).GetComponent<Slider>();

        fadeInOut = GetComponent<UI_FadeInOut>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(fadeInOut.FadeIn(canvasGroup));
    }

    private void Awake()
    {
        SetUp();
    }
}
