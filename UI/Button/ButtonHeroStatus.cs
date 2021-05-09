using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHeroStatus : ButtonUnderHud, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventdata)
    {
        if(!buttonActive)
        {
            buttonActive = true;
            UnderHudManager.instance.ChangeCanvas(UnderHudManager.UnderInfo.HEROINFO);
        }
    }

    private void Awake()
    {
        SetUp();
        ChangeActivate(false);
    }
}
