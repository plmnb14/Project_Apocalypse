using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUnderHud : MonoBehaviour, IPointerClickHandler
{
    private const int buttonCount = 2;

    public UnderHudManager.UnderInfo infoType;
    protected GameObject[] childButton;
    public bool buttonActive { get; set; }

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (!buttonActive)
        {
            buttonActive = true;
            UnderHudManager.instance.ChangeCanvas(infoType);
        }
    }

    public void OnClickButton()
    {
        Debug.Log("´­¸²");
    }

    public void ChangeActivate(bool value)
    {
        childButton[0].SetActive(!value);
        childButton[1].SetActive(value);
        buttonActive = value;
    }

    protected virtual void SetUp()
    {
        childButton = new GameObject[buttonCount];
        for(int i = 0; i < buttonCount; i++)
        {
            childButton[i] = transform.GetChild(i).gameObject;
        }

        buttonActive = false;
    }

    private void Awake()
    {
        SetUp();
        ChangeActivate(false);
    }
}
