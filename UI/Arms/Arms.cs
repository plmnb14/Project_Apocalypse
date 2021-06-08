using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmsStatus
{
    public int tier;
    public int heldCount;
}

public class Arms : MonoBehaviour, IPointerClickHandler
{
    #region Click Events
    public void OnPointerClick(PointerEventData eventData)
    {
        ArmsManager.Instance.PopUpArmsMount();
    }
    #endregion
}
