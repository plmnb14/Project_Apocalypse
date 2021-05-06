using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonsterHP : UI_HPBar
{
    private Vector3 calibrationVec;
    private Vector3 oldVector;

    protected override void OnDie()
    {
        base.OnDie();

        UIManager.instance.BackHpBar(myName, this);
    }

    private void UpdatePosition()
    {
        rectTrans.position = targetTransform.position + calibrationVec;
        oldVector = rectTrans.position - calibrationVec;
    }

    public override void ResetStatus(Transform parent = null)
    {
        base.ResetStatus();

        oldVector = Vector3.zero;
    }

    protected override void SetUp()
    {
        base.SetUp();

        HpSlider = rectTrans.GetChild(0).GetComponent<Slider>();

        calibrationVec = Vector3.up * 0.65f;
        oldVector = Vector3.zero;
    }

    private void Update()
    {
        if (oldVector != targetTransform.position) 
            UpdatePosition();
    }

    private void Start()
    {
        transform.position = targetTransform.position + calibrationVec;
        transform.localScale = Vector3.one;
    }

    private void Awake()
    {
        SetUp();
    }
}
