using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Object
{
    public Transform targetTransform { get; set; }
    protected Slider HpSlider;
    protected RectTransform rectTrans;

    public override void ResetStatus(Transform parent = null)
    {
        base.ResetStatus();

        targetTransform = null;
        HpSlider.value = 0.0f;
        HpSlider.maxValue = 1.0f;
        rectTrans.position = Vector3.zero;
    }

    public virtual void ChangeHealth(float health)
    {
        HpSlider.value = health;

        if (health <= 0.0f)
        {
            HpSlider.value = 0.0f;
            OnDie();
        }
    }

    public void SetUpHealth(float health)
    {
        HpSlider.maxValue = health;
        HpSlider.value = health;
    }

    protected virtual void SetUp()
    {
        rectTrans = GetComponent<RectTransform>();
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Awake()
    {
        SetUp();
    }
}
