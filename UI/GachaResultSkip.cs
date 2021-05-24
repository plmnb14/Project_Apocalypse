using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GachaResultSkip : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FixedMenuManager.instance.gachaResultPopUp.SkipGachaResult();
        this.gameObject.SetActive(false);
    }
}
