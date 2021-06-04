using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePopUp : MonoBehaviour, IPointerClickHandler
{
    public bool isCloseOnlyNew;

    public void OnPointerClick(PointerEventData eventData)
    {
        PopUpManager.instance.RemovePopUp(isCloseOnlyNew);
    }
}
