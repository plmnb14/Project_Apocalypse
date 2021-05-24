using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedMenuButton : MonoBehaviour, IPointerClickHandler
{
    public FixedMenu fixedMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        FixedMenuManager.instance.ChangeCurrentFixedMenu(fixedMenu);
    }
}
