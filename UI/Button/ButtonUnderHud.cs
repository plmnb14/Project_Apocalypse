using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUnderHud : MonoBehaviour
{
    protected GameObject[] childButton;
    public bool buttonActive { get; set; }

    public void ChangeActivate(bool value)
    {
        childButton[0].SetActive(!value);
        childButton[1].SetActive(value);
        buttonActive = value;
    }

    protected virtual void SetUp()
    {
        childButton = new GameObject[2];
        childButton[0] = transform.GetChild(0).gameObject;
        childButton[1] = transform.GetChild(1).gameObject;

        buttonActive = false;
    }
}
